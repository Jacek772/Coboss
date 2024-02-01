using AutoMapper;
using Coboss.Application.Functions.Commands.BusinnessTaskRealisations;
using Coboss.Core.Entities;
using Coboss.Types.DTO;

namespace Coboss.Application.MappingProfiles
{
    public class BusinnessTaskRealisationMappingProfile : Profile
    {
        public BusinnessTaskRealisationMappingProfile()
        {
            CreateMap<BusinnessTaskRealisation, BusinnessTaskRealisationDTO>()
                .ForMember(m => m.Employee, x => x.MapFrom(y => y.Employee));
            CreateMap<CreateBusinnessTaskRealisationCommand, BusinnessTaskRealisation>();
        }
    }
}
