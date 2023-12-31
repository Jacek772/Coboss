using Coboss.Core.Entities.Abstracts;
using Microsoft.AspNetCore.Identity;

namespace Coboss.Core.Entities
{
    public class User : BaseEntitiy
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public byte[] Salt { get; set; } = default!;

        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; } = default!;
    }
}
