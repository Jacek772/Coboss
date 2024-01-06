using AutoMapper;
using Coboss.Application.Functions.Commands.BusinnessTaskComments;
using Coboss.Core.Entities;
using Coboss.Types.DTO;

namespace Coboss.Application.MappingProfiles
{
    public class BusinnessTaskCommentMappingProfile : Profile
    {
        public BusinnessTaskCommentMappingProfile()
        {
            CreateMap<BusinnessTaskComment, BusinnessTaskCommentDTO>()
                .ForMember(m => m.User, x => x.MapFrom(y => y.User));
            CreateMap<CreateBusinnessTaskCommentCommand, BusinnessTaskComment>();
        }
    }
}