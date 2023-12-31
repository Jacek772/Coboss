using Coboss.Application.Functions.Commands;
using Coboss.Application.Services.Abstracts;
using MediatR;

namespace Coboss.Application.Functions.CommandHandlers
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, Unit>
    {
        private readonly IEmployeesService _employeesService;

        public DeleteEmployeeCommandHandler(IEmployeesService employeesService)
        {
            _employeesService = employeesService;
        }

        public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            await _employeesService.DeleteEmployeeAsync(request.Id);
            return Unit.Value;
        }
    }
}