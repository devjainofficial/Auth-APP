using CQRSMediator.Abstractions;
using Microsoft.Extensions.Logging;
using Philbor.Application.Abstractions;
using Philbor.Domain.Shared;

namespace Philbor.Application.Features.Auth.ConfirmEmail
{
    public class AcceptInvitationHandler(IAuthService authService,
        ILogger<AcceptInvitationHandler> logger,
        IHelperService helperService) : IRequestHandler<AcceptInvitationCommand, Result>
    {
        public async Task<Result> HandleAsync(AcceptInvitationCommand command, CancellationToken cancellationToken)
        {
            return await helperService.ReturnResultAsync(async () =>
            {
                var result = await authService.AcceptInvitationAsync(command);
                return result;

            }, logger);
        }

    }
}
