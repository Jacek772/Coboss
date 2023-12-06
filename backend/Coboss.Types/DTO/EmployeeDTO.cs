using Coboss.Core.Entities;

namespace Coboss.Types.DTO
{
    public class EmployeeDTO
    {
        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public string? PESEL { get; set; }
        public string? NIP { get; set; }

        public User? User { get; set; }
        public ICollection<EmployeeHistory> EmployeeHistories { get; set; } = default!;
    }
}
