using Coboss.Core.Entities;

namespace Coboss.Types.DTO
{
    public class BusinnessTaskDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTime Date { get; set; }
        public DateTime Term { get; set; }
        public int ProjectId { get; set; } = default!;

        public ICollection<BusinnessTaskRealisationDTO> TaskRealisations { get; set; } = new List<BusinnessTaskRealisationDTO>();
        public List<BusinnessTaskCommentDTO> Comments { get; set; } = new List<BusinnessTaskCommentDTO>();
        public List<EmployeeDTO> Employees { get; set; } = new List<EmployeeDTO>();
    }
}