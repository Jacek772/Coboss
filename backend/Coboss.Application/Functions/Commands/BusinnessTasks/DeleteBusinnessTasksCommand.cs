using MediatR;

namespace Coboss.Application.Functions.Commands.BusinnessTasks
{
    public class DeleteBusinnessTasksCommand : IRequest<Unit>
    {
        public int[] Ids { get; set; } = default!;
    }
}
