namespace Coboss.Types.DTO
{
    public class BusinnessTaskCommentDTO
    {
        public string Text { get; set; } = default!;
        public DateTime Date { get; set; }
        public UserDTO User { get; set; } = default!;
    }
}
