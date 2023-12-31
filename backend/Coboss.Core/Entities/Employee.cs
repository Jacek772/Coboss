using Coboss.Core.Entities.Abstracts;

namespace Coboss.Core.Entities
{
    public class Employee : BaseEntitiy
    {
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public string? PESEL { get; set; }
        public string? NIP { get; set; }
        public DateTime DateOfBirth { get; set; } = default!;

        public User? User { get; set; }
        public ICollection<EmployeeHistory> EmployeeHistories { get; set; } = default!;
    }
}
