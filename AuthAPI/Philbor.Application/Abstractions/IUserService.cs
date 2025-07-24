using Philbor.Application.Features.User.Create;
using Philbor.Domain.Shared;

namespace Philbor.Application.Abstractions
{
    public interface IUserService
    {
        Task<Result> CreateUserAsync(CreateUserCommand command);
    }
}
