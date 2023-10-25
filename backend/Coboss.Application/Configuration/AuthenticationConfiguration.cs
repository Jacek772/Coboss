using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coboss.Application.Configuration
{
    public class AuthenticationConfiguration
    {
        public string JwtKey { get; set; }
        public int JwtExpireDays { get; set; }
        public string JwtIssuer { get; set; }
        public int SaltSize { get; set; }
        public string AdminLogin { get; set; }
        public string AdminPassword { get; set; }
        public string AdminEmail { get; set; }
    }
}
