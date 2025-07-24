using CQRSMediator.Abstractions;
using Philbor.Domain.Shared;

namespace Philbor.Application.Features.Auth.ForgotPassword
{
    public class ForgotPasswordQuery : IRequest<Result>
    {
        public required string Email { get; set; }
    }
}
