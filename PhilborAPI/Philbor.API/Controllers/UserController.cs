using CQRSMediator.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Philbor.Application.Features.User.Create;

namespace Philbor.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController(IDispatcher dispatcher) : ApiControllerBase
    {
        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            return ApiResult(await dispatcher.SendAsync(command));
        }
    }
}
