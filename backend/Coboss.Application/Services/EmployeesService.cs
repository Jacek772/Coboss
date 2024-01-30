using Coboss.Application.Extensions;
using Coboss.Application.Functions.Commands.Employees;
using Coboss.Application.Functions.Query.Employees;
using Coboss.Application.Services.Abstracts;
using Coboss.Core.Entities;
using Coboss.Persistance;
using Coboss.Types.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Coboss.Application.Services
{
    public class EmployeesService : IEmployeesService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IGlobalSettingsService _globalSettingsService;
        private readonly IObjectCodesService _objectCodesService;

        public EmployeesService(ApplicationDbContext applicationDbContext,
            IGlobalSettingsService globalSettingsService, IObjectCodesService objectCodesService)
        {
            _applicationDbContext = applicationDbContext;
            _globalSettingsService = globalSettingsService;
            _objectCodesService = objectCodesService;
        }

        public async Task<Employee> GetAsync(int id)
        {
            return await _applicationDbContext.Employees
                .Include(x => x.User)
                .Include(x => x.EmployeeHistories)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Employee>> GetAsync(GetEmployeesQuery getEmployeesQuery)
        {
            IQueryable<Employee> employees = _applicationDbContext.Employees
                 .Include(x => x.User)
                 .Include(x => x.EmployeeHistories);

            if (!string.IsNullOrEmpty(getEmployeesQuery?.SearchText))
            {
                employees = employees
                    .Where(x => x.Name.ToLower().Contains(getEmployeesQuery.SearchText.ToLower())
                        || x.Surname.ToLower().Contains(getEmployeesQuery.SearchText.ToLower())
                        || (x.NIP != null && x.NIP.ToLower().Contains(getEmployeesQuery.SearchText.ToLower()))
                        || (x.PESEL != null && x.PESEL.ToLower().Contains(getEmployeesQuery.SearchText.ToLower()))
                    );
            }

            if (getEmployeesQuery?.FirstId != null)
            {
                employees = employees
                    .Where(x => x.Id > getEmployeesQuery.FirstId);
            }

            if (getEmployeesQuery?.Size is int size)
            {
                employees = employees
                    .Take(size);
            }

            if (!string.IsNullOrEmpty(getEmployeesQuery?.OrderBy) && !string.IsNullOrEmpty(getEmployeesQuery?.OrderBy))
            {
                employees = employees.ApplaySort(getEmployeesQuery.OrderBy);
            }
            return await employees.ToListAsync();
        }

        public async Task<List<Employee>> GetAsync()
        {
            return await _applicationDbContext.Employees
                .Include(x => x.User)
                .Include(x => x.EmployeeHistories)
                .ToListAsync();
        }

        public async Task CreateAsync(Employee employee)
        {
            employee.Code = await GetNewEmployeeCode();

            using (IDbContextTransaction transaction = _applicationDbContext.Database.BeginTransaction())
            {
                try
                {
                    await _applicationDbContext.Employees.AddAsync(employee);
                    await _applicationDbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception($"Employee create error\n{ex.ToMessage()}");
                }
            }
        }

        public async Task CreateAsync(IEnumerable<Employee> employees)
        {
            foreach (Employee employee in employees)
            {
                employee.Code = await GetNewEmployeeCode();
            }

            using (IDbContextTransaction transaction = _applicationDbContext.Database.BeginTransaction())
            {
                try
                {
                    await _applicationDbContext.Employees.AddRangeAsync(employees);
                    await _applicationDbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception($"Employee create error\n{ex.ToMessage()}");
                }
            }
        }

        public async Task CreateHistoryAsync(EmployeeHistory employeeHistory)
        {
            using (IDbContextTransaction transaction = _applicationDbContext.Database.BeginTransaction())
            {
                try
                {
                    await _applicationDbContext.EmployeeHistories.AddAsync(employeeHistory);
                    await _applicationDbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception($"EmployeeHistory create error\n{ex.ToMessage()}");
                }
            }
        }

        public async Task UpdateAsync(UpdateEmployeeCommand command)
        {
            Employee employee = await _applicationDbContext.Employees.FirstOrDefaultAsync(x => x.Id == command.Id);
            if (employee == null)
            {
                throw new Exception($"Employee with id = {command.Id} not exits");
            }

            using (IDbContextTransaction transaction = _applicationDbContext.Database.BeginTransaction())
            {
                try
                {
                    if (command.Name is string name)
                    {
                        employee.Name = name;
                    }

                    if (command.Surname is string surname)
                    {
                        employee.Surname = surname;
                    }

                    if (command.NIP is string nip)
                    {
                        employee.NIP = nip;
                    }

                    if (command.PESEL is string pesel)
                    {
                        employee.PESEL = pesel;
                    }

                    if (command.DateOfBirth is DateTime dateOfBirth)
                    {
                        employee.DateOfBirth = dateOfBirth;
                    }

                    await _applicationDbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception($"Employee update error\n{ex.ToMessage()}");
                }
            }
        }

        public async Task<bool> ExistsAsync()
        {
            return await _applicationDbContext.Employees.AnyAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Employee employee = await _applicationDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (employee == null)
            {
                throw new BadRequestException($"Employee with id = {id} not exits");
            }

            using (IDbContextTransaction transaction = await _applicationDbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    _applicationDbContext.Employees.Remove(employee);
                    await _applicationDbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception($"Employee delete error\n{ex.ToMessage()}");
                }
            }
        }

        public async Task DeleteAsync(int[] ids)
        {
            List<Employee> employees = await _applicationDbContext.Employees
                    .Where(x => ids.Contains(x.Id))
                    .ToListAsync();

            if (employees.Count == 0)
            {
                throw new BadRequestException($"Employees with passed ids not exits");
            }

            using (IDbContextTransaction transaction = await _applicationDbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    _applicationDbContext.Employees.RemoveRange(employees);
                    await _applicationDbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception($"Employee delete error\n{ex.ToMessage()}");
                }
            }
        }

        private async Task<string> GetNewEmployeeCode()
        {
            int codeLength = await _globalSettingsService
                .GetValueIntAsync(GlobalSetting.GlobalSettingKey.EmployeeCodeLength);

            ObjectCode objectCode = await _objectCodesService.GetNewObjectCode<Employee>(codeLength);
            return objectCode.Code;
        }
    }
}