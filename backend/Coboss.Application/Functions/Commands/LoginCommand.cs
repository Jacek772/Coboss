using Coboss.Types.DTO;
using MediatR;

namespace Coboss.Application.Functions.Commands
{
    public class LoginCommand : IRequest<AuthenticationResultDTO>
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}