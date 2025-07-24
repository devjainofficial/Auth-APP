using Philbor.Application.Features.Auth.ConfirmEmail;
using Philbor.Application.Features.Auth.ForgotPassword;
using Philbor.Application.Features.Auth.Login;
using Philbor.Application.Features.Auth.ResetPassword;
using Philbor.Application.Features.Auth.VerifyTwoFactorToken;
using Philbor.Domain.Models;
using Philbor.Domain.Shared;

namespace Philbor.Application.Abstractions
{
    public interface IAuthService
    {
        Task<Result> LoginUserAsync(LoginQuery query);
        Task<IList<string>> GetUsersRolesAsync(ApplicationUser user);
        (string Token, DateTime Expiration) GetAccessToken(ApplicationUser user, IList<string> userRoles);
        Task<Result> AcceptInvitationAsync(AcceptInvitationCommand command);
        Task<Result> ForgotPasswordAsync(ForgotPasswordQuery query);
        Task<Result> ResetPasswordAsync(ResetPasswordQuery query);
        Task<Result> VerifyTwoFactorToken(VerifyTwoFactorTokenQuery query);
    }
}
