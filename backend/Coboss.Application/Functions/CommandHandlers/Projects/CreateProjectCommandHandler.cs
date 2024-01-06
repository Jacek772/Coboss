using AutoMapper;
using Coboss.Application.Functions.Commands.Projects;
using Coboss.Application.Services.Abstracts;
using Coboss.Core.Entities;
using Coboss.Types.Exceptions;
using MediatR;

namespace Coboss.Application.Functions.CommandHandlers.Projects
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IProjectsService _projectsService;
        private readonly IEmployeesService _employeesService;

        public CreateProjectCommandHandler(IMapper mapper,
            IProjectsService projectsService, IEmployeesService employeesService)
        {
            _mapper = mapper;
            _projectsService = projectsService;
            _employeesService = employeesService;
        }

        public async Task<Unit> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            Employee manager = await _employeesService.GetAsync(request.ManagerId);
            if(manager == null)
            {
                throw new BadRequestException($"Employee with id = {request.ManagerId} cannot exists.");
            }

            Project project = _mapper.Map<CreateProjectCommand, Project>(request);
            project.Manager = manager;
            await _projectsService.CreateAsync(project);
            return Unit.Value;
        }
    }
}
