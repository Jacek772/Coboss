using Coboss.Core.Entities.Abstracts;

namespace Coboss.Core.Entities
{
    public class BusinnessTaskComment : BaseEntitiy
    {
        public string Text { get; set; } = default!;
        public DateTime Date { get; set; }
        public User User { get; set; } = default!;
    }
}