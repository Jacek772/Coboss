using Coboss.Types.DTO;
using MediatR;

namespace Coboss.Application.Functions.Query.BusinnessTasks
{
    public class GetBusinnessTasksQuery : IRequest<List<BusinnessTaskDTO>>
    {
        public string? SearchText { get; set; }
        public string? OrderBy { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public DateTime? TermFrom { get; set; }
        public DateTime? TermTo { get; set; }
        public int? ProjectId { get; set; }
    }
}
