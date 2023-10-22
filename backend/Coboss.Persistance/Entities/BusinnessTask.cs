using Coboss.Persistance.Entities.Abstracts;

namespace Coboss.Persistance.Entities
{
    public class BusinnessTask : BaseEntitiy
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateOnly Date { get; set; }
        public DateOnly Term { get; set; }

        public ICollection<BusinnessTaskRealisation> BusinnessTaskRealisations { get; set; }
        public ICollection<Attachment> Attachments { get; set; }
    }
}
