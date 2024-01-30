using Coboss.Application.Functions.Commands.BusinnessTasks;
using Coboss.Application.Functions.Query.BusinnessTasks;
using Coboss.Controllers.Abstracts;
using Coboss.Types.DTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coboss.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class BusinnessTasksController : ApiControllerBase
    {
        public BusinnessTasksController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<ActionResult<List<BusinnessTaskDTO>>> GetBusinnessTasks([FromQuery] GetBusinnessTasksQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<ActionResult> CreateBusinnessTask([FromBody] CreateBusinnessTaskCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<ActionResult> UpdateBusinnessTask([FromBody] UpdateBusinnessTaskCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<ActionResult> DeleteBusinnessTasks([FromBody] DeleteBusinnessTasksCommand command)
        {
            
            await _mediator.Send(command);
            return Ok();
        }
    }
}
