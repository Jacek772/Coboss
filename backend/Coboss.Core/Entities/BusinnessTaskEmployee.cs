namespace Coboss.Core.Entities
{
    public class BusinnessTaskEmployee
    {
        public int BusinnessTaskId { get; set; }
        public BusinnessTask BusinnessTask { get; set; } = default!;

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = default!;
    }
}
