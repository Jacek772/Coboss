using AutoMapper;
using Coboss.Application.Functions.Query;
using Coboss.Application.Services.Abstracts;
using Coboss.Core.Entities;
using Coboss.Types.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coboss.Application.Functions.QueryHandlers
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
            User user = await _usersService.GetUserByIdAsync(request.UserId);
            return _mapper.Map<UserDTO>(user);
        }
    }
}
