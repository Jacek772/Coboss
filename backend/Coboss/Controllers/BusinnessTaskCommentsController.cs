using Coboss.Application.Functions.Commands.BusinnessTaskComments;
using Coboss.Application.Functions.Query.BusinnessTaskComments;
using Coboss.Controllers.Abstracts;
using Coboss.Types.DTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coboss.Controllers
{
    [Route("api/taskcomments")]
    [ApiController]
    public class BusinnessTaskCommentsController : BaseApiController
    {
        public BusinnessTaskCommentsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<ActionResult<List<BusinnessTaskCommentDTO>>> GetBusinnessTaskComments([FromQuery] GetBusinnessTaskCommentsQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<ActionResult> CreateBusinnessTaskComment([FromBody] CreateBusinnessTaskCommentCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<ActionResult> UpdateBusinnessTaskRealisation([FromBody] UpdateBusinnessTaskCommentCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<ActionResult> DeleteBusinnessTaskRealisations([FromBody] DeleteBusinnessTaskCommentsCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}
