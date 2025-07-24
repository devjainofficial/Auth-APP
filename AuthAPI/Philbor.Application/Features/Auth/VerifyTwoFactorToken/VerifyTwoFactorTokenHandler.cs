using CQRSMediator.Abstractions;
using Mapster;
using Microsoft.Extensions.Logging;
using Philbor.Application.Abstractions;
using Philbor.Application.Features.Auth.Login;
using Philbor.Domain.Errors;
using Philbor.Domain.Models;
using Philbor.Domain.Shared;

namespace Philbor.Application.Features.Auth.VerifyTwoFactorToken
{
    public class VerifyTwoFactorTokenHandler(IAuthService authService,
        ILogger<VerifyTwoFactorTokenHandler> logger,
        IHelperService helperService) : IRequestHandler<VerifyTwoFactorTokenQuery, Result>
    {
        public async Task<Result> HandleAsync(VerifyTwoFactorTokenQuery query, CancellationToken cancellationToken)
        {
            return await helperService.ReturnResultAsync(async () =>
            {
                var result = await authService.VerifyTwoFactorToken(query);
                
                if (!result.IsSuccess)
                {
                    return Result.Failure(ClientErrors.InvalidToken);
                }
                
                ApplicationUser user = (ApplicationUser)result.Data!;
                IList<string> userRoles = await authService.GetUsersRolesAsync(user);
                var userResult = user.Adapt<UserResult>();
                userResult.Roles = userRoles;

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
