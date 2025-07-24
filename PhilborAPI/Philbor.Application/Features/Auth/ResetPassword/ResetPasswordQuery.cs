using CQRSMediator.Abstractions;
using Philbor.Domain.Shared;

namespace Philbor.Application.Features.Auth.ResetPassword
{
    public class ResetPasswordQuery : IRequest<Result>
    {
        public required string Email { get; set; }
        public required string ResetPasswordToken { get; set; }
        public required string Password { get; set; }
    }
}
