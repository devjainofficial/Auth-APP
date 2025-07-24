using CQRSMediator.Abstractions;
using Philbor.Domain.Shared;

namespace Philbor.Application.Features.User.Create
{
    public class CreateUserCommand : IRequest<Result>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public bool Enable2FA { get; set; }
    }
}
