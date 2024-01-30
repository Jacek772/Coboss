using Coboss.Application.Functions.Commands.Projects;
using Coboss.Application.Functions.Query.Projects;
using Coboss.Controllers.Abstracts;
using Coboss.Types.DTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coboss.Controllers
{
    [Route("api/projects")]
    [Authorize]
    public class ProjectsController : ApiControllerBase
    {
        public ProjectsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<ActionResult<List<ProjectDTO>>> GetProjects([FromQuery] GetProjectsQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<ActionResult> CreateProject([FromBody] CreateProjectCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<ActionResult> UpdateProject([FromBody] UpdateProjectCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<ActionResult> DeleteProjects([FromBody] DeleteProjectsCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}
