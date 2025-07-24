using CQRSMediator.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Philbor.Application.Features.Auth.ConfirmEmail;
using Philbor.Application.Features.Auth.ForgotPassword;
using Philbor.Application.Features.Auth.Login;
using Philbor.Application.Features.Auth.ResetPassword;
using Philbor.Application.Features.Auth.VerifyTwoFactorToken;

namespace Philbor.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IDispatcher dispatcher) : ApiControllerBase
    {
        [HttpPost("login-user")]
        public async Task<IActionResult> LoginUser([FromBody] LoginQuery query)
        {
            return ApiResult(await dispatcher.SendAsync(query));
        }

        [HttpPost("accept-invitation")]
        public async Task<IActionResult> AcceptInvitation([FromBody] AcceptInvitationCommand command)
        {
            return ApiResult(await dispatcher.SendAsync(command));
        }

        [HttpGet("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromQuery] ForgotPasswordQuery query)
        {
            return ApiResult(await dispatcher.SendAsync(query));
        }

        [HttpGet("verify-twoFactor-token")]
        public async Task<IActionResult> VerifyTwoFactorToken([FromQuery] VerifyTwoFactorTokenQuery query)
        {
            return ApiResult(await dispatcher.SendAsync(query));
        }

        [HttpGet("reset-password")]
        public async Task<IActionResult> ResetPassword([FromQuery] ResetPasswordQuery query)
        {
            return ApiResult(await dispatcher.SendAsync(query));
        }
    }
}
