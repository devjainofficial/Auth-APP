using CQRSMediator.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Philbor.Application.Features.ExternalAPI;

namespace Philbor.API.Controllers
{
    [Route("api/external")]
    [ApiController]
    public class ExternalController(IDispatcher dispatcher) : ApiControllerBase
    {
        [HttpGet("get-users")]
        public async Task<IActionResult> GetUsers()
        {
            return ApiResult(await dispatcher.SendAsync(new ExternalAPIQuery { }));
        }
    }
}
