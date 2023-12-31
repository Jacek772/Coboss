using Coboss.Application.Functions.Commands;
using Coboss.Application.Services.Abstracts;
using MediatR;

namespace Coboss.Application.Functions.CommandHandlers
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
            await _employeesService.DeleteEmployeesAsync(request.Ids);
            return Unit.Value;
        }
    }
}
