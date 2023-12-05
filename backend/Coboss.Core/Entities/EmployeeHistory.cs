using Coboss.Core.Entities.Abstracts;

namespace Coboss.Core.Entities
{
    public class EmployeeHistory : BaseEntitiy
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public decimal CostHourOfWork { get; set; }
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; } = default!;
    }
}
