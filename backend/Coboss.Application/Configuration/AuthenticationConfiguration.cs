using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coboss.Application.Configuration
{
    public class AuthenticationConfiguration
    {
        public string JwtKey { get; set; } = default!;
        public int JwtExpireDays { get; set; }
        public string JwtIssuer { get; set; } = default!;
        public string JwtAudience { get; set; } = default!;
        public int SaltSize { get; set; }
        public string AdminPassword { get; set; } = default!;
        public string AdminEmail { get; set; } = default!;
        public string AdminRoleName { get; set; } = default!;
    }
}
