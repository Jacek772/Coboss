using Coboss.Core.Entities;

namespace Coboss.Types.DTO
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public string? PESEL { get; set; }
        public string? NIP { get; set; }
        public DateTime DateOfBirth { get; set; } = default!;

        public UserDTO? User { get; set; }
        public List<EmployeeHistoryDTO> EmployeeHistories { get; set; } = default!;
    }
}
