namespace Coboss.Types.DTO
{
    public class BusinnessTaskRealisationDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan TimeSpan { get; set; }
        public string? Description { get; set; }
    }
}
