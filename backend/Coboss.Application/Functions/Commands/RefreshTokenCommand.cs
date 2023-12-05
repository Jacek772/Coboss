using Coboss.Types.DTO;
using MediatR;

namespace Coboss.Application.Functions.Commands
{
    public class RefreshTokenCommand : IRequest<AuthenticationResultDTO>
    {
        public string Token { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
    }
}
