using Coboss.Core.Entities.Abstracts;

namespace Coboss.Core.Entities
{
    public class User : BaseEntitiy
    {
        public string Login { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Email { get; set; } = default!;
        public byte[] Salt { get; set; } = default!;

        public int? EmployeeID { get; set; }
        public Employee? Employee { get; set; }
    }
}
