using CQRSMediator.Abstractions;
using Philbor.Domain.Shared;

namespace Philbor.Application.Features.Auth.ConfirmEmail
{
    public class AcceptInvitationCommand : IRequest<Result>
    {
        public string Email { get; set; }
        public string Token { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }
}
