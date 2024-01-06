using Coboss.Types.DTO;
using MediatR;

namespace Coboss.Application.Functions.Query.Users
{
    public class GetUserQuery : IRequest<UserDTO>
    {
        public int UserId { get; set; }
    }
}
