using Coboss.Application.Functions.Commands.Employees;
using Coboss.Application.Functions.Query.Employees;
using Coboss.Core.Entities;

namespace Coboss.Application.Services.Abstracts
{
    public interface IEmployeesService
    {
        Task<bool> ExistsAsync();
        Task<Employee> GetAsync(int id);
        Task<List<Employee>> GetAsync();
        Task<List<Employee>> GetAsync(GetEmployeesQuery getEmployeesQuery);
        Task CreateAsync(Employee employee);
        Task CreateAsync(IEnumerable<Employee> employees);
        Task CreateHistoryAsync(EmployeeHistory employeeHistory);
        Task UpdateAsync(UpdateEmployeeCommand updateEmployeeCommand);
        Task DeleteAsync(int id);
        Task DeleteAsync(int[] ids);
    }
}
