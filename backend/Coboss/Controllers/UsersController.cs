using Coboss.Application.Functions.Query;
using Coboss.Application.Functions.Query.Users;
using Coboss.Controllers.Abstracts;
using Coboss.Types.DTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coboss.Controllers
{
    [Route("api/users")]
    [Authorize]
    public class UsersController : ApiControllerBase
    {
        public UsersController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("current")]
        public async Task<UserDTO> GetCurrentLoggedUser()
        {
            return await _mediator.Send(new GetUserQuery() { UserId = GetCurrentUserId() });
        }
    }}
