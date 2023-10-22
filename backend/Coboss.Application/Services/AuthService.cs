using Coboss.Application.Services.Abstracts;
using Coboss.Types.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coboss.Application.Services
{
    public class AuthService : IAuthService
    {
        public LoginResultDTO Login(LoginDTO loginDTO) 
        {
            return new LoginResultDTO { };
        }
    }
}
