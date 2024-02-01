using Coboss.Core.Entities.Abstracts;

namespace Coboss.Core.Entities
{
    public class BusinnessTask : BaseEntitiy
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTime Date { get; set; }
        public DateTime Term { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; } = default!;

        public ICollection<BusinnessTaskRealisation> TaskRealisations { get; set; } = default!;
        public ICollection<BusinnessTaskComment> Comments { get; set; } = default!;
        public ICollection<BusinnessTaskEmployee> BusinnessTasksEmployees { get; set; } = default!;
    }
}
