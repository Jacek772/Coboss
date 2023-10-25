using Coboss.Application.Functions.Commands;
using Coboss.Controllers.Abstracts;
using Coboss.Types.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Coboss.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        public AuthController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResultDTO>> Login([FromBody] LoginCommand loginCommand)
        {
            return await _mediator.Send(loginCommand);
        }
    }
}
