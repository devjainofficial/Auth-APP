using CQRSMediator.Abstractions;
using Philbor.Domain.Shared;

namespace Philbor.Application.Features.Auth.VerifyTwoFactorToken
{
    public class VerifyTwoFactorTokenQuery : IRequest<Result>
    {
        public required string Email { get; set; }
        public required string VerificationToken { get; set; }
    }
}
