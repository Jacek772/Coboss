using Coboss.Core.Entities;

namespace Coboss.Application.Services.Abstracts
{
    public interface IEmployeesService
    {
        Task<List<Employee>> GetEmployeesAsync();
    }
}
