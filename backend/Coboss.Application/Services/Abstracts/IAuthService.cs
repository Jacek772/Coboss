using Coboss.Types.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coboss.Application.Services.Abstracts
{
    public interface IAuthService
    {
        LoginResultDTO Login(LoginDTO loginDTO);
    }
}
