using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Philbor.Application.Abstractions;
using Philbor.Application.Features.Auth.ConfirmEmail;
using Philbor.Application.Features.Auth.ForgotPassword;
using Philbor.Application.Features.Auth.Login;
using Philbor.Application.Features.Auth.ResetPassword;
using Philbor.Application.Features.Auth.VerifyTwoFactorToken;
using Philbor.Domain.Errors;
using Philbor.Domain.Models;
using Philbor.Domain.Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace Philbor.Infrastructure.Services
{
    public class AuthService(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IConfiguration configuration,
        IEmailTemplateService emailTemplateService,
        IMailService mailService) : IAuthService
    {
        #region LoginUser
        public async Task<Result> LoginUserAsync(LoginQuery query)
        {
            ApplicationUser? user = await userManager.FindByEmailAsync(query.Email);

            if (user == null) return Result.Failure(ClientErrors.UserNotFound);

            SignInResult result = await signInManager.PasswordSignInAsync(user, query.Password, false, false);

            if (result.Succeeded && !user.EmailConfirmed)
            {
                return Result.Failure(ClientErrors.UserAccountNotVerified);
            }

            if (!result.Succeeded && result.IsLockedOut)
                return Result.Failure(ClientErrors.UserLockout);

            if (!result.Succeeded && !result.RequiresTwoFactor)
                return Result.Failure(ClientErrors.InvalidCredentials);

            if (result.RequiresTwoFactor)
                await SendTwoFactorTokenAsync(user);

            return Result.Success(user, "Login Successfully");
        }
        #endregion

        #region SendTwoFactorToken
        //Used to send two factor token on user email address
        public async Task<Result> SendTwoFactorTokenAsync(ApplicationUser user)
        {
            IList<string> providers = await userManager.GetValidTwoFactorProvidersAsync(user);
            if (!providers.Contains("Email")) return Result.Failure(ClientErrors.InvalidProvider);  // Internet server error

            await userManager.UpdateSecurityStampAsync(user);
            string code = await userManager.GenerateTwoFactorTokenAsync(user, "Email");
            string twoFactorToken = HttpUtility.UrlEncode(code); // Authentication token

            string mailBody = emailTemplateService.Get2FATemplate(user.FirstName, twoFactorToken);

            List<string> emails = new List<string>();
            emails.Add(user.Email!);
            Result result = await mailService.SendEmailAsync(new()
            {
                Body = mailBody,
                Subject = "Here’s Your 2FA Code to Sign In",
                ToEmails = emails
            });

            if (result.IsFailure) return result;

            return Result.Success(message: "Two factor authentication code has been sent to your registered email address");
        }
        #endregion

        #region GetUsersRoles
        public async Task<IList<string>> GetUsersRolesAsync(ApplicationUser user)
        {
            return await userManager.GetRolesAsync(user);
        }
        #endregion

        public (string Token, DateTime Expiration) GetAccessToken(ApplicationUser user, IList<string> userRoles)
        {
            List<Claim> authClaims = new List<Claim>
                            {
                                new Claim(AppConsts.ClaimTypes.UserNameClaim, user.UserName!),
                                new Claim(AppConsts.ClaimTypes.EmailClaim, user.Email!),
                                new Claim(AppConsts.ClaimTypes.UserFullNameClaim, $"{user.FirstName} {user.LastName}"),
                                new Claim(ClaimTypes.Name, user.Id.ToString()),
                                new Claim(AppConsts.ClaimTypes.UserIdClaim, user.Id.ToString()),
                                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            };


            foreach (string roleClaim in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, roleClaim));
            }

            JwtSecurityToken authToken = GetToken(authClaims, DateTime.Now.AddMinutes(15));
            string token = new JwtSecurityTokenHandler().WriteToken(authToken);

            return (token, authToken.ValidTo);
        }

        #region GetToken Method
        public JwtSecurityToken GetToken(List<Claim> authClaims, DateTime expiration)
        {
            // Get the secret key from configuration for signing the JWT
            SymmetricSecurityKey authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]!));

            // Create signing credentials using the HMAC-SHA256 algorithm and the secret key
            SigningCredentials credentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256);

            // Create the JWT security token with issuer, audience, claims, and signing credentials
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: expiration,
                claims: authClaims,
                signingCredentials: credentials
            );

            // Return the generated token
            return token;
        }
        #endregion

        #region AcceptInvitation
        public async Task<Result> AcceptInvitationAsync(AcceptInvitationCommand command)
        {
            ApplicationUser? user = await userManager.FindByEmailAsync(command.Email);

            if (user == null) return Result.Failure(ClientErrors.UserNotFound);

            command.Token = command.Token.Replace(" ", "+");

            IdentityResult result = await userManager.ConfirmEmailAsync(user, command.Token);

            if (!result.Succeeded)
                return Result.Failure(ClientErrors.IdentityError(result.Errors));

            user.FirstName = command.FirstName;
            user.LastName = command.LastName;

            await userManager.UpdateAsync(user);

            IdentityResult response = await userManager.ChangePasswordAsync(user, "Test@123", command.Password);

            if (response.Succeeded) return Result.Success(user.Adapt<UserResult>(), "Invitation Accepted Successfully");

            return Result.Failure(ClientErrors.IdentityError(response.Errors));
        }
        #endregion

        public async Task<Result> ForgotPasswordAsync(ForgotPasswordQuery query)
        {
            ApplicationUser? user = await userManager.FindByEmailAsync(query.Email);

            if (user == null) return Result.Failure(ClientErrors.UserNotFound);

            string code = await userManager.GeneratePasswordResetTokenAsync(user);
            string passwordToken = HttpUtility.UrlEncode(code);
            string resetPasswordLink = configuration["WebBaseUrl"] + "auth/reset-password?email=" + query.Email + "&passwordToken=" + passwordToken;

            string mailBody = emailTemplateService.GetSendPasswordResetLinkTemplate(user.UserName!, resetPasswordLink);

            List<string> emails = new();
            emails.Add(query.Email);
            Result result = await mailService.SendEmailAsync(new()
            {
                Body = mailBody,
                Subject = "Password Reset Link",
                ToEmails = emails
            });

            if (result.IsFailure) return result;
            return Result.Success(true, "Password reset link has been sent to email address");
        }

        public async Task<Result> ResetPasswordAsync(ResetPasswordQuery query)
        {
            ApplicationUser? user = await userManager.FindByEmailAsync(query.Email);

            if (user == null) return Result.Failure(ClientErrors.UserNotFound);

            IdentityResult result = await userManager.ResetPasswordAsync(user, query.ResetPasswordToken, query.Password);

            if (!result.Succeeded)
                return Result.Failure(ClientErrors.IdentityError(result.Errors));

            return Result.Success(true, "Password reset successfully");
        }

        public async Task<Result> VerifyTwoFactorToken(VerifyTwoFactorTokenQuery query)
        {
            ApplicationUser? user = await userManager.FindByEmailAsync(query.Email);

            if (user == null) return Result.Failure(ClientErrors.UserNotFound);

            bool result = await userManager.VerifyTwoFactorTokenAsync(user, "Email", query.VerificationToken);

            if (!result)
            {
                return Result.Failure(ClientErrors.InvalidToken);
            }

            return Result.Success(user, "Token has been verified");
        }
    }
}
