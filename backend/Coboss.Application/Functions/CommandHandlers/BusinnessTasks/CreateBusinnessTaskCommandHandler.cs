using AutoMapper;
using Coboss.Application.Functions.Commands.BusinnessTasks;
using Coboss.Application.Services.Abstracts;
using Coboss.Core.Entities;
using Coboss.Types.Exceptions;
using MediatR;

namespace Coboss.Application.Functions.CommandHandlers.BusinnessTasks
{
    public class CreateBusinnessTaskCommandHandler : IRequestHandler<CreateBusinnessTaskCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IProjectsService _projectsService;

        public CreateBusinnessTaskCommandHandler(IMapper mapper,
            IBusinnessTasksService businnessTasksService,
            IProjectsService projectsService)
        {
            _mapper = mapper;
            _projectsService = projectsService;
        }

        public async Task<Unit> Handle(CreateBusinnessTaskCommand request, CancellationToken cancellationToken)
        {
            Project project = await _projectsService.GetByIdAsync(request.ProjectId);
            if (project == null)
            {
                throw new BadRequestException($"Project with id = {request.ProjectId} cannot exists.");
            }
            BusinnessTask businnessTask = _mapper.Map<CreateBusinnessTaskCommand, BusinnessTask>(request);
            project.BusinnessTasks.Add(businnessTask);
            await _projectsService.UpdateAsync(project);
            return Unit.Value;
        }
    }
}
