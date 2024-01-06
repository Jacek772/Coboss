using AutoMapper;
using Coboss.Application.Functions.Query.Users;
using Coboss.Application.Services.Abstracts;
using Coboss.Core.Entities;
using Coboss.Types.DTO;
using MediatR;

namespace Coboss.Application.Functions.QueryHandlers.Users
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDTO>
    {
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;

        public GetUserQueryHandler(IUsersService usersService, IMapper mapper)
        {
            _usersService = usersService;
            _mapper = mapper;
        }

        public async Task<UserDTO> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            User user = await _usersService.GetByIdAsync(request.UserId);
            return _mapper.Map<User, UserDTO>(user);
        }
    }
}
