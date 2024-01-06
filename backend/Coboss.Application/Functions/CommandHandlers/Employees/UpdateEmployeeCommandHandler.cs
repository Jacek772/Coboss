using Coboss.Application.Functions.Commands.Employees;
using Coboss.Application.Services.Abstracts;
using MediatR;

namespace Coboss.Application.Functions.CommandHandlers.Employees
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Unit>
    {
        private readonly IEmployeesService _employeesService;

        public UpdateEmployeeCommandHandler(IEmployeesService employeesService)
        {
            _employeesService = employeesService;
        }

        public async Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            await _employeesService.UpdateAsync(request);
            return Unit.Value;
        }
    }
}
