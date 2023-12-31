using MediatR;

namespace Coboss.Application.Functions.Commands
{
    public class DeleteEmployeeCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}