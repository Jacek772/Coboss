using Coboss.Types.DTO;
using MediatR;

namespace Coboss.Application.Functions.Query.BusinnessTasks
{
    public class GetBusinnessTasksQuery : IRequest<List<BusinnessTaskDTO>>
    {
        public string? SearchText { get; set; }
        public string? OrderBy { get; set; }
    }
}
