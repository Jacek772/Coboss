using Coboss.Persistance.Entities;
using Coboss.Persistance.Entities.Abstracts;

namespace Coboss.Persistance.Entities
{
    public class Project : BaseEntitiy
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<BusinnessTask> BusinnessTasks { get; set; }
    }
}
