using MediatR;

namespace Coboss.Application.Functions.Commands.Employees
{
    public class DeleteEmployeeCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}