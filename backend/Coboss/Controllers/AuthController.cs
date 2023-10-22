using Coboss.Controllers.Abstracts;
using Coboss.Types.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Coboss.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        [HttpPost("login")]
        public async Task<ActionResult<LoginResultDTO>> Login([FromBody] LoginDTO loginData)
        {
            //LoginCommand loginCommand = _mapper.Map<LoginRequest, LoginCommand>(loginRequest);
            //return await _mediator.Send(loginCommand);
            return new LoginResultDTO
            {
                Token = ""
            };
        }
    }
}
