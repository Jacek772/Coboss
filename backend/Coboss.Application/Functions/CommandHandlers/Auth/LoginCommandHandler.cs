using Coboss.Application.Functions.Commands.Auth;
using Coboss.Application.Services.Abstracts;
using Coboss.Core.Entities;
using Coboss.Types.DTO;
using Coboss.Types.Exceptions;
using MediatR;

namespace Coboss.Application.Functions.CommandHandlers.Auth
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthenticationResultDTO>
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

        public async Task<AuthenticationResultDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            User user = await _usersService.GetByEmailAsync(request.Email);
            if (user is null)
            {
                throw new BadRequestException("Bad user email or password");
            }

            if (!_passwordHasherService.ComparePasswordHash(request.Password, user))
            {
                throw new BadRequestException("Bad user email or password");
            }

            return await _authService.GenerateTokenAsync(user);
        }
    }
}
