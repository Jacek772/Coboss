using AutoMapper;
using Coboss.Application.Functions.Query.Employees;
using Coboss.Application.Services.Abstracts;
using Coboss.Core.Entities;
using Coboss.Types.DTO;
using MediatR;

namespace Coboss.Application.Functions.QueryHandlers.Employees
{
    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, List<EmployeeDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IEmployeesService _employeesService;

        public GetEmployeesQueryHandler(IMapper mapper, IEmployeesService employeesService)
        {
            _mapper = mapper;
            _employeesService = employeesService;
        }

        public async Task<List<EmployeeDTO>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            List<Employee> employees = await _employeesService.GetAsync(request);
            return _mapper.Map<List<Employee>, List<EmployeeDTO>>(employees);
        }
    }
}
