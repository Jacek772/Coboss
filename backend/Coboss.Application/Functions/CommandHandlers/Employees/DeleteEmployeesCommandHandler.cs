using Coboss.Application.Functions.Commands.Employees;
using Coboss.Application.Services.Abstracts;
using MediatR;

namespace Coboss.Application.Functions.CommandHandlers.Employees
{
    public class DeleteEmployeesCommandHandler : IRequestHandler<DeleteEmployeesCommand, Unit>
    {
        private readonly IEmployeesService _employeesService;

        public DeleteEmployeesCommandHandler(IEmployeesService employeesService)
        {
            _employeesService = employeesService;
        }

        public async Task<Unit> Handle(DeleteEmployeesCommand request, CancellationToken cancellationToken)
        {
            await _employeesService.DeleteAsync(request.Ids);
            return Unit.Value;
        }
    }
}
