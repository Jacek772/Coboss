using MediatR;

namespace Coboss.Application.Functions.Commands
{
    public class DeleteEmployeesCommand : IRequest<Unit>
    {
        public int[] Ids { get; set; } = default!;
    }
}
