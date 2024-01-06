using Coboss.Application.Functions.Commands.Projects;
using Coboss.Application.Services.Abstracts;
using MediatR;

namespace Coboss.Application.Functions.CommandHandlers.Projects
{
    public class DeleteProjectsCommandHandler : IRequestHandler<DeleteProjectsCommand, Unit>
    {
        private readonly IProjectsService _projectsService;

        public DeleteProjectsCommandHandler(IProjectsService projectsService)
        {
            _projectsService = projectsService;
        }

        public async Task<Unit> Handle(DeleteProjectsCommand request, CancellationToken cancellationToken)
        {
            await _projectsService.DeleteAsync(request.Ids);
            return Unit.Value;
        }
    }
}
