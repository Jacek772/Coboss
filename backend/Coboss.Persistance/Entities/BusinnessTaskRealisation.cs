using Coboss.Persistance.Entities;
using Coboss.Persistance.Entities.Abstracts;

namespace Coboss.Persistance.Entities
{
    public class BusinnessTaskRealisation : BaseEntitiy
    {
        public DateTime Date {  get; set; }
        public TimeSpan TimeSpan { get; set; }
        public string? Description { get; set; }
    }
}


