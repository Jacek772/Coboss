using MediatR;

namespace Coboss.Application.Functions.Commands.Employees
{
    public class DeleteEmployeesCommand : IRequest<Unit>
    {
        public int[] Ids { get; set; } = default!;
    }
}
