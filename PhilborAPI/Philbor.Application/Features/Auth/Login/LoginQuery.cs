using CQRSMediator.Abstractions;
using Philbor.Domain.Shared;

namespace Philbor.Application.Features.Auth.Login
{
    public class LoginQuery : IRequest<Result>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
