using MediatR;

namespace Coboss.Application.Functions.Commands.BusinnessTaskComments
{
    public class DeleteBusinnessTaskCommentsCommand : IRequest<Unit>
    {
        public int[] Ids { get; set; } = default!;
    }
}
