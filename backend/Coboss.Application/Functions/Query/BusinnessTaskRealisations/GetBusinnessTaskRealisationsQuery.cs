using Coboss.Types.DTO;
using MediatR;

namespace Coboss.Application.Functions.Query.BusinnessTaskRealisations
{
    public class GetBusinnessTaskRealisationsQuery : IRequest<List<BusinnessTaskRealisationDTO>>
    {
        public string? SearchText { get; set; }
        public string? OrderBy { get; set; }
    }
}
