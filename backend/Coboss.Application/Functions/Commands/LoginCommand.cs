using Coboss.Types.DTO;
using MediatR;

namespace Coboss.Application.Functions.Commands
{
    public class LoginCommand : IRequest<LoginResultDTO>
    {
        public string Login { get; set; }

        public string Password { get; set; }
    }
}
