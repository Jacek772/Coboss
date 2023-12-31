using AutoMapper;
using Coboss.Application.Functions.Commands;
using Coboss.Application.Services.Abstracts;
using Coboss.Core.Entities;
using MediatR;

namespace Coboss.Application.Functions.CommandHandlers
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IEmployeesService _employeesService;

        public CreateEmployeeCommandHandler(IMapper mapper, IEmployeesService employeesService)
        {
            _mapper = mapper;
            _employeesService = employeesService;
        }

        public async Task<Unit> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            Employee employee = _mapper.Map<CreateEmployeeCommand, Employee>(request);
            await _employeesService.CreateEmployeeAsync(employee);
            return Unit.Value;
        }
    }
}
