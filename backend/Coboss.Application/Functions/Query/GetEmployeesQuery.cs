using Coboss.Types.DTO;
using MediatR;

namespace Coboss.Application.Functions.Query
{
    public class GetEmployeesQuery : IRequest<List<EmployeeDTO>>
    {
        public string? SearchText { get; set; }
        public int? FirstId { get; set; }
        public int? Size { get; set; }
        public string? OrderBy { get; set; }
    }
}
