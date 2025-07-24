using CQRSMediator.Abstractions;
using Microsoft.Extensions.Logging;
using Philbor.Application.Abstractions;
using Philbor.Domain.Shared;

namespace Philbor.Application.Features.Auth.ForgotPassword
{
    public class ForgotPasswordHandler(IAuthService authService,
        ILogger<ForgotPasswordHandler> logger,
        IHelperService helperService) : IRequestHandler<ForgotPasswordQuery, Result>
    {
        public async Task<Result> HandleAsync(ForgotPasswordQuery query, CancellationToken cancellationToken)
        {
            return await helperService.ReturnResultAsync(async () =>
            {
                var result = await authService.ForgotPasswordAsync(query);
                return result;

            }, logger);
        }
    }
}
