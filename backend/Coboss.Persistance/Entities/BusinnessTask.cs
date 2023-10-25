using Coboss.Persistance.Entities.Abstracts;

namespace Coboss.Persistance.Entities
{
    public class BusinnessTask : BaseEntitiy
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public DateTime Term { get; set; }

        public ICollection<BusinnessTaskRealisation> BusinnessTaskRealisations { get; set; }
        public ICollection<Attachment> Attachments { get; set; }
    }
}
