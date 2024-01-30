using Coboss.Application.Functions.Commands.Auth;
using Coboss.Controllers.Abstracts;
using Coboss.Types.DTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coboss.Controllers
{
    [Route("api/auth")]
    public class AuthController : ApiControllerBase
    {
        public AuthController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("signin")]
        public async Task<ActionResult<AuthenticationResultDTO>> Signin([FromBody] LoginCommand loginCommand)
        {
            return await _mediator.Send(loginCommand);
        }

        [HttpPost("refresh")]
        public async Task<ActionResult<AuthenticationResultDTO>> Refresh([FromBody] RefreshTokenCommand refreshTokenCommand)
        {
            return await _mediator.Send(refreshTokenCommand);
        }

        [Authorize]
        [HttpGet("logged")]
        public ActionResult CheckIsLogged()
        {
            return Ok(new { Logged = true });
        }
    }
}
