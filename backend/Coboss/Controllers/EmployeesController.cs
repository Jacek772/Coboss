using Coboss.Application.Functions.Commands.Employees;
using Coboss.Application.Functions.Query.Employees;
using Coboss.Controllers.Abstracts;
using Coboss.Types.DTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coboss.Controllers
{
    [Route("api/employees")]
    [Authorize]
    public class EmployeesController : BaseApiController
    {
        public EmployeesController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<ActionResult<List<EmployeeDTO>>> GetEmployees([FromQuery] GetEmployeesQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<ActionResult> CreateEmployee([FromBody] CreateEmployeeCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<ActionResult> UpdateEmployee([FromBody] UpdateEmployeeCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("one/{id}")]
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<ActionResult> DeleteEmployee([FromRoute] int id)
        {
            DeleteEmployeeCommand command = new DeleteEmployeeCommand
            {
                Id = id
            };
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("many")]
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<ActionResult> DeleteEmployees([FromBody] DeleteEmployeesCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}
