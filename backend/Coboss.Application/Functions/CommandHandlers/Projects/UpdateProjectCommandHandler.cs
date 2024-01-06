using Coboss.Application.Functions.Commands.Projects;
using Coboss.Application.Services.Abstracts;
using MediatR;

namespace Coboss.Application.Functions.CommandHandlers.Projects
{
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Unit>
    {
        private readonly IProjectsService _projectsService;

        public UpdateProjectCommandHandler(IProjectsService projectsService)
        {
            _projectsService = projectsService;
        }

        public async Task<Unit> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            await _projectsService.UpdateAsync(request);
            return Unit.Value;
        }
    }
}
