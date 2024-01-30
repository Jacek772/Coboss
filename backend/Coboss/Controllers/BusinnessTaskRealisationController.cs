using Coboss.Application.Functions.Commands.BusinnessTaskRealisations;
using Coboss.Application.Functions.Query.BusinnessTaskRealisations;
using Coboss.Controllers.Abstracts;
using Coboss.Types.DTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coboss.Controllers
{
    [Route("api/taskrealisations")]
    [ApiController]
    public class BusinnessTaskRealisationsController : ApiControllerBase
    {
        public BusinnessTaskRealisationsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<ActionResult<List<BusinnessTaskRealisationDTO>>> GetBusinnessTaskRealisations([FromQuery] GetBusinnessTaskRealisationsQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<ActionResult> CreateBusinnessTaskRealisation([FromBody] CreateBusinnessTaskRealisationCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<ActionResult> UpdateBusinnessTaskRealisation([FromBody] UpdateBusinnessTaskRealisationCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<ActionResult> DeleteBusinnessTaskRealisations([FromBody] DeleteBusinnessTaskRealisationsCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}
