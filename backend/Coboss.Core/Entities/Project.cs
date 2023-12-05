using Coboss.Core.Entities.Abstracts;

namespace Coboss.Core.Entities
{
    public class Project : BaseEntitiy
    {
        public string Number { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTime Term { get; set; } = default!;

        public Employee Manager { get; set; } = default!;
        public ICollection<BusinnessTask> BusinnessTasks { get; set; } = default!;
    }
}
