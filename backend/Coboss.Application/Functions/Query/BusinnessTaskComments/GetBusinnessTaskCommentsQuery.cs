using Coboss.Types.DTO;
using MediatR;

namespace Coboss.Application.Functions.Query.BusinnessTaskComments
{
    public class GetBusinnessTaskCommentsQuery : IRequest<List<BusinnessTaskCommentDTO>>
    {
        public string? SearchText { get; set; }
        public string? OrderBy { get; set; }
    }
}
