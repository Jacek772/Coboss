using MediatR;

namespace Coboss.Application.Functions.Commands.BusinnessTaskRealisations
{
    public class DeleteBusinnessTaskRealisationsCommand : IRequest<Unit>
    {
        public int[] Ids { get; set; } = default!;
    }
}
