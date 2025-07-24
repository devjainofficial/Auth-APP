using CQRSMediator.Abstractions;
using Microsoft.Extensions.Logging;
using Philbor.Application.Abstractions;
using Philbor.Domain.Shared;

namespace Philbor.Application.Features.User.Create
{
    public class CreateUserHandler(IUserService userService,
        ILogger<CreateUserHandler> logger,
        IHelperService helperService) : IRequestHandler<CreateUserCommand, Result>
    {
        public async Task<Result> HandleAsync(CreateUserCommand command, CancellationToken cancellationToken)
        {
            return await helperService.ReturnResultAsync(async () =>
            {
                var result = await userService.CreateUserAsync(command);
                return result;
            }, logger);
        }
    }
}
