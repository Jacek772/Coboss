namespace Coboss.Types.DTO
{
    public class BusinnessTaskDTO
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTime Date { get; set; }
        public DateTime Term { get; set; }

        public ICollection<BusinnessTaskRealisationDTO> TaskRealisations { get; set; } = new List<BusinnessTaskRealisationDTO>();
        public List<BusinnessTaskCommentDTO> Comments { get; set; } = new List<BusinnessTaskCommentDTO>();
    }
} 