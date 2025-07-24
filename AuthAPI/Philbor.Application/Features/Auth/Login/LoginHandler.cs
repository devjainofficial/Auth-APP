using CQRSMediator.Abstractions;
using Mapster;
using Microsoft.Extensions.Logging;
using Philbor.Application.Abstractions;
using Philbor.Domain.Models;
using Philbor.Domain.Shared;

namespace Philbor.Application.Features.Auth.Login
{
    public class LoginHandler(IAuthService authService,
        ILogger<LoginHandler> logger,
        IHelperService helperService) : IRequestHandler<LoginQuery, Result>
    {
        public async Task<Result> HandleAsync(LoginQuery query, CancellationToken cancellationToken)
        {
            return await helperService.ReturnResultAsync(async () =>
            {
                var loginResult = await authService.LoginUserAsync(query);

                if (loginResult.IsFailure) return loginResult;

                ApplicationUser user = (ApplicationUser)loginResult.Data!;
                IList<string> userRoles = await authService.GetUsersRolesAsync(user);
                var userResult = user.Adapt<UserResult>();
                userResult.Roles = userRoles;

                if (user.TwoFactorEnabled)
                {
                    return Result.Success(new LoginResult
                    {
                        Token = null,
                        Expiration = null,
                        User = userResult
                    });
                }

                (string token, DateTime expiration) = authService.GetAccessToken(user, userRoles);

                return Result.Success(new LoginResult
                {
                    Token = token,
                    Expiration = expiration,
                    User = userResult
                });

            }, logger);
        }
    }
}
