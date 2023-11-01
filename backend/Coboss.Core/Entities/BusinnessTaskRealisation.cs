using Coboss.Core.Entities.Abstracts;

namespace Coboss.Core.Entities
{
    public class BusinnessTaskRealisation : BaseEntitiy
    {
        public DateTime Date {  get; set; }
        public TimeSpan TimeSpan { get; set; }
        public string? Description { get; set; }
    }
}


