using Coboss.Controllers.Abstracts;
using Coboss.Types.DTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coboss.Controllers
{
    [Route("api/users")]
    [Authorize]
    public class UsersController : BaseApiController
    {
        public UsersController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public ActionResult GetAllUsers()
        {
            UserDTO[] userDTOs = new UserDTO[]
            {
                new UserDTO { Email = "user1@email.com", Login = "user1", Name = "Joe", Surname = "Doe" },
                new UserDTO { Email = "user2@email.com", Login = "user2", Name = "Ela", Surname = "Kowalska" },
                new UserDTO { Email = "user3@email.com", Login = "user3", Name = "Johny", Surname = "Rambo" },
            };

            return Ok(userDTOs);
        }
    }}
