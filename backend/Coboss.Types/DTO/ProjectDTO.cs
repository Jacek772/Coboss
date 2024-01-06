namespace Coboss.Types.DTO
{
    public class ProjectDTO
    {
        public string Number { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTime Term { get; set; } = default!;
        public EmployeeDTO Manager { get; set; } = default!;
        public ICollection<BusinnessTaskDTO> BusinnessTasks { get; set; } = default!;
    }
}
