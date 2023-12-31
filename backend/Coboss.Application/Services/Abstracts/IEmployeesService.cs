using Coboss.Application.Functions.Commands;
using Coboss.Application.Functions.Query;
using Coboss.Core.Entities;

namespace Coboss.Application.Services.Abstracts
{
    public interface IEmployeesService
    {
        Task<bool> EmployeesExistsAsync();
        Task<List<Employee>> GetEmployeesAsync();
        Task<List<Employee>> GetEmployeesAsync(GetEmployeesQuery getEmployeesQuery);
        Task CreateEmployeeAsync(Employee employee);
        Task CreateEmployeesAsync(IEnumerable<Employee> employees);
        Task CreateEmployeeHistoryAsync(EmployeeHistory employeeHistory);
        Task UpdateEmployeeAsync(UpdateEmployeeCommand updateEmployeeCommand);
        Task DeleteEmployeeAsync(int id);
        Task DeleteEmployeesAsync(int[] ids);
    }
}
