using Coboss.Core.Entities.Abstracts;
using Microsoft.AspNetCore.Identity;

namespace Coboss.Core.Entities
{
    public class User : BaseEntitiy
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public byte[] Salt { get; set; } = default!;

        public Guid? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; } = default!;
    }
}
