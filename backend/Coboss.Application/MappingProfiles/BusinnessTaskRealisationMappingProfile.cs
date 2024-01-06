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
            CreateMap<BusinnessTaskRealisation, BusinnessTaskRealisationDTO>();
            CreateMap<CreateBusinnessTaskRealisationCommand, BusinnessTaskRealisation>();
        }
    }
}
