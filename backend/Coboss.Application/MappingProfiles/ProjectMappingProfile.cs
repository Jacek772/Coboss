using AutoMapper;
using Coboss.Application.Functions.Commands.Projects;
using Coboss.Core.Entities;
using Coboss.Types.DTO;

namespace Coboss.Application.MappingProfiles
{
    public class ProjectMappingProfile : Profile
    {
        public ProjectMappingProfile() 
        {
            CreateMap<Project, ProjectDTO>()
                .ForMember(m => m.Manager, x => x.MapFrom(y => y.Manager))
                .ForMember(m => m.BusinnessTasks, x => x.MapFrom(y => y.BusinnessTasks));
            CreateMap<CreateProjectCommand, Project>();
        }
    }
}
