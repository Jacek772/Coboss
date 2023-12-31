using Coboss.Application.Functions.Commands;
using Coboss.Application.Services.Abstracts;
using MediatR;

namespace Coboss.Application.Functions.CommandHandlers
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
            await _employeesService.UpdateEmployeeAsync(request);
            return Unit.Value;
        }
    }
}
