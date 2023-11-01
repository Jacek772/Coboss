using Coboss.Core.Entities.Abstracts;

namespace Coboss.Core.Entities
{
    public class BusinnessTask : BaseEntitiy
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTime Date { get; set; }
        public DateTime Term { get; set; }

        public ICollection<BusinnessTaskRealisation> BusinnessTaskRealisations { get; set; }
        public ICollection<Attachment> Attachments { get; set; }
    }
}
