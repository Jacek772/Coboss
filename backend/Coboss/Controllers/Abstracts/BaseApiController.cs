using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

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
            if(int.TryParse(User?.FindFirst("Id")?.Value, out int id))
            {
                return id;
            }
            return 0;
        }
    }
}
