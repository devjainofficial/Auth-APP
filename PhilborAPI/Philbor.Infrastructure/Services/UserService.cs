using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Philbor.Application.Abstractions;
using Philbor.Application.Features.Auth.Login;
using Philbor.Application.Features.User.Create;
using Philbor.Domain.Errors;
using Philbor.Domain.Models;
using Philbor.Domain.Shared;

namespace Philbor.Infrastructure.Services
{
    public class UserService(IUserContext userContext,
        UserManager<ApplicationUser> userManager,
        IConfiguration configuration,
        IMailService mailService,
        IEmailTemplateService emailTemplateService) : IUserService
    {
        #region CreateUser
        //Used to register user using sign up page
        public async Task<Result> CreateUserAsync(CreateUserCommand command)
        {
            ApplicationUser? user = await userManager.FindByEmailAsync(command.Email);

            if (user != null) return Result.Failure(ClientErrors.EmailAlreadyExists);

            user = await userManager.FindByNameAsync(command.UserName);

            if (user != null) return Result.Failure(ClientErrors.UserNameAlreadyExists);

            ApplicationUser applicationUser = new ApplicationUser
            {
                UserName = command.UserName,
                FirstName = command.FirstName,
                LastName = command.LastName,
                Gender = command.Gender,
                Email = command.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                TwoFactorEnabled = command.Enable2FA,
                EmailConfirmed = false,
                InvitationId = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                CreatedBy = userContext.UserId
            };

            IdentityResult result = await userManager.CreateAsync(applicationUser, "Test@123");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(applicationUser, "User");
                await SendConfirmationEmailAsync(applicationUser!);
                var userResult = applicationUser.Adapt<UserResult>();
                userResult.Roles = ["User"];
                return Result.Success(userResult, "User Created Succssfully");
            }

            IdentityError error = result.Errors.First();

            if (result.Errors.Count() > 0) return Result.Failure(new Error(error.Code, error.Description));
            else return Result.Failure(ClientErrors.SomethingWentWrong);
        }
        #endregion

        public async Task SendConfirmationEmailAsync(ApplicationUser user)
        {
            string token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            string confirmationLink = configuration["WebBaseUrl"] + "auth/invitation?email=" + user.Email + "&token=" + token + "&invitationId=" + user.InvitationId;
            string mailBody = emailTemplateService.GetInvitationTemplate(user.FirstName, confirmationLink);
            List<string> emails = new();
            emails.Add(user.Email!);
            await mailService.SendEmailAsync(new()
            {
                Body = mailBody,
                Subject = "Confirm Your Email",
                ToEmails = emails
            });
        }
    }
}
