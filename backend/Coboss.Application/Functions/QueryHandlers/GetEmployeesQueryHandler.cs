using AutoMapper;
using Coboss.Application.Functions.Query;
using Coboss.Application.Services.Abstracts;
using Coboss.Core.Entities;
using Coboss.Types.DTO;
using MediatR;

namespace Coboss.Application.Functions.QueryHandlers
{
    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, List<EmployeeDTO>>
    {
        private readonly IEmployeesService _employeesService;
        private readonly IMapper _mapper;

        public GetEmployeesQueryHandler(IEmployeesService employeesService, IMapper mapper)
        {
            _employeesService = employeesService;
            _mapper = mapper;
        }

        public async Task<List<EmployeeDTO>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            List<Employee> employees = await _employeesService.GetEmployeesAsync(request);
            return _mapper.Map<List<EmployeeDTO>>(employees);
        }
    }
}
