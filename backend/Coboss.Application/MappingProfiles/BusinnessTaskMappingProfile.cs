﻿using AutoMapper;
using Coboss.Application.Functions.Commands.BusinnessTasks;
using Coboss.Core.Entities;
using Coboss.Types.DTO;

namespace Coboss.Application.MappingProfiles
{
    public class BusinnessTaskMappingProfile : Profile
    {
        public BusinnessTaskMappingProfile()
        {
            CreateMap<BusinnessTask, BusinnessTaskDTO>()
                .ForMember(m => m.TaskRealisations, x => x.MapFrom(y => y.TaskRealisations))
                .ForMember(m => m.Comments, x => x.MapFrom(y => y.Comments));
            CreateMap<CreateBusinnessTaskCommand, BusinnessTask>();
        }
    }
}