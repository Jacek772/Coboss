using Coboss.Application.Functions.Commands;
using Coboss.Application.Services.Abstracts;
using Coboss.Core.Entities;
using Coboss.Types.DTO;
using Coboss.Types.Exceptions;
using MediatR;

namespace Coboss.Application.Functions.CommandHandlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResultDTO>
    {
        private readonly IUsersService _usersService;
        private readonly IAuthService _authService;
        private readonly IPasswordHasherService _passwordHasherService;

        public LoginCommandHandler(IUsersService usersService,
            IAuthService authService,
            IPasswordHasherService passwordHasherService)
        {
            _usersService = usersService;
            _authService = authService;
            _passwordHasherService = passwordHasherService;
        }

        public async Task<LoginResultDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            User user = await _usersService.GetUserByLoginAsync(request.Login);
            if (user is null)
            {
                throw new BadRequestException("Bad user login or password");
            }

            if(!_passwordHasherService.ComparePasswordHash(request.Password, user))
            {
                throw new BadRequestException("Bad user login or password");
            }

            return new LoginResultDTO
            {
                Token = _authService.GenerateToken(user)
            };
        }
    }
}
