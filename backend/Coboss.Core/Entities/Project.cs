using Coboss.Core.Entities.Abstracts;

namespace Coboss.Core.Entities
{
    public class Project : BaseEntitiy
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;

        public ICollection<BusinnessTask> BusinnessTasks { get; set; } = default!;
    }
}
