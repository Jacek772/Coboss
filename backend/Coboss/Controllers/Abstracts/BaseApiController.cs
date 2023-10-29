using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Coboss.Controllers.Abstracts
{
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]
    public abstract class BaseApiController : Controller
    {
        protected readonly IMediator _mediator;

        public BaseApiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected int GetCurrentUserId()
        {
            if(int.TryParse(User?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int id))
            {
                return id;
            }
            return 0;
        }
    }
}
