using CQRSMediator.Abstractions;
using Microsoft.Extensions.Logging;
using Philbor.Application.Abstractions;
using Philbor.Domain.Shared;

namespace Philbor.Application.Features.Auth.ResetPassword
{
    public class ResetPasswordHandler(IAuthService authService,
        ILogger<ResetPasswordHandler> logger,
        IHelperService helperService) : IRequestHandler<ResetPasswordQuery, Result>
    {
        public async Task<Result> HandleAsync(ResetPasswordQuery query, CancellationToken cancellationToken)
        {
            return await helperService.ReturnResultAsync(async () =>
            {
                var result = await authService.ResetPasswordAsync(query);
                return result;

            }, logger);
        }
    }
}
