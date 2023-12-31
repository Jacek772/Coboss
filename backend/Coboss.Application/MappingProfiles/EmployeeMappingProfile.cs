using AutoMapper;
using Coboss.Application.Functions.Commands;
using Coboss.Core.Entities;
using Coboss.Types.DTO;

namespace Coboss.Application.MappingProfiles
{
    public class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
            CreateMap<Employee, EmployeeDTO>()
                .ForMember(m => m.User, x => x.MapFrom(y => y.User))
                .ForMember(m => m.EmployeeHistories, x => x.MapFrom(y => y.EmployeeHistories.ToList()));

            CreateMap<CreateEmployeeCommand, Employee>();
        }
    }
}

