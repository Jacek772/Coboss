using AutoMapper;
using Coboss.Core.Entities;
using Coboss.Types.DTO;

namespace Coboss.Application.MappingProfiles
{
    public class EmployeeHistoryMappingProfile : Profile
    {
        public EmployeeHistoryMappingProfile()
        {
            CreateMap<EmployeeHistory, EmployeeHistoryDTO>();
        }
    }
}
