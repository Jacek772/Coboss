﻿using Coboss.Types.DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Coboss.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
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