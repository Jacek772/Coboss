using AutoMapper;
using Coboss.Application.Functions.Query.Projects;
using Coboss.Application.Services.Abstracts;
using Coboss.Core.Entities;
using Coboss.Types.DTO;
using MediatR;

namespace Coboss.Application.Functions.QueryHandlers.Projects
{
    public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, List<ProjectDTO>>
    {
        private readonly IProjectsService _projectsService;
        private readonly IMapper _mapper;

        public GetProjectsQueryHandler(IProjectsService projectsService, IMapper mapper)
        {
            _projectsService = projectsService;
            _mapper = mapper;
        }

        public async Task<List<ProjectDTO>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
        {
            List<Project> projects = await _projectsService.GetAsync(request);
            return _mapper.Map<List<Project>, List<ProjectDTO>>(projects);
        }
    }
}
