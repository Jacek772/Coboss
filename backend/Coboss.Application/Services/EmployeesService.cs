using Coboss.Application.Services.Abstracts;
using Coboss.Core.Entities;
using Coboss.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Coboss.Application.Services
{
    public class EmployeesService : IEmployeesService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public EmployeesService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return await _applicationDbContext.Employees
                .Include(x => x.User)
                .Include(x => x.EmployeeHistories)
                .ToListAsync();
        }
    }
}
