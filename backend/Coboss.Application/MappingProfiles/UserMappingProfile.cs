using AutoMapper;
using Coboss.Persistance.Entities;
using Coboss.Types.DTO;

namespace Coboss.Application.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDTO>();
        }
    }
}
