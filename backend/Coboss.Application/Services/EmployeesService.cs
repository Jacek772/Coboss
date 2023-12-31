using Coboss.Application.Extensions;
using Coboss.Application.Functions.Commands;
using Coboss.Application.Functions.Query;
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

        public EmployeesService(ApplicationDbContext applicationDbContext,
            IGlobalSettingsService globalSettingsService)
        {
            _applicationDbContext = applicationDbContext;
            _globalSettingsService = globalSettingsService;
        }

        public async Task<List<Employee>> GetEmployeesAsync(GetEmployeesQuery getEmployeesQuery)
        {
            IQueryable<Employee> employees = _applicationDbContext.Employees
                 .Include(x => x.User)
                 .Include(x => x.EmployeeHistories);

            if (!string.IsNullOrEmpty(getEmployeesQuery?.SearchText))
            {
                employees = employees
                    .Where(x => EF.Functions.ILike(x.Name, $"%{getEmployeesQuery.SearchText}%")
                       || EF.Functions.ILike(x.Surname, $"%{getEmployeesQuery.SearchText}%")
                       || EF.Functions.ILike(x.PESEL, $"%{getEmployeesQuery.SearchText}%")
                       || EF.Functions.ILike(x.NIP, $"%{getEmployeesQuery.SearchText}%"));
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

            if(!string.IsNullOrEmpty(getEmployeesQuery?.OrderBy) && !string.IsNullOrEmpty(getEmployeesQuery?.OrderBy))
            {
                employees = employees.ApplaySort(getEmployeesQuery.OrderBy);
            }

            return await employees.ToListAsync();
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return await _applicationDbContext.Employees
                .Include(x => x.User)
                .Include(x => x.EmployeeHistories)
                .ToListAsync();
        }

        public async Task CreateEmployeeAsync(Employee employee)
        {
            EmployeeCode employeeCode = await GetNewEmployeeCode();
            employee.Code = employeeCode.Code;

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

        public async Task CreateEmployeesAsync(IEnumerable<Employee> employees)
        {
            foreach (Employee employee in employees)
            {
                EmployeeCode employeeCode = await GetNewEmployeeCode();
                employee.Code = employeeCode.Code;
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

        public async Task CreateEmployeeHistoryAsync(EmployeeHistory employeeHistory)
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

        public async Task UpdateEmployeeAsync(UpdateEmployeeCommand updateEmployeeCommand)
        {
            Employee employee = await _applicationDbContext.Employees.FirstOrDefaultAsync(x => x.Id == updateEmployeeCommand.Id);
            if (employee == null)
            {
                throw new Exception($"Employee with id = {updateEmployeeCommand.Id} not exits");
            }

            using (IDbContextTransaction transaction = _applicationDbContext.Database.BeginTransaction())
            {
                try
                {
                    if (updateEmployeeCommand.Name is string name)
                    {
                        employee.Name = name;
                    }

                    if (updateEmployeeCommand.Surname is string surname)
                    {
                        employee.Surname = surname;
                    }

                    if (updateEmployeeCommand.NIP is string nip)
                    {
                        employee.NIP = nip;
                    }

                    if (updateEmployeeCommand.PESEL is string pesel)
                    {
                        employee.PESEL = pesel;
                    }

                    if(updateEmployeeCommand.DateOfBirth is DateTime dateOfBirth)
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

        private async Task<EmployeeCode> GetNewEmployeeCode()
        {
            EmployeeCode employeeCodeLast = await _applicationDbContext.EmployeeCodes
                .OrderByDescending(x => x.CodeNumber)
                .FirstOrDefaultAsync();

            int codeLength = await _globalSettingsService
                .GetGlobalSettingValueIntAsync(GlobalSetting.GlobalSettingKey.EmployeeCodeLength);

            EmployeeCode employeeCode;
            if (employeeCodeLast == null)
            {
                employeeCode = new EmployeeCode
                {
                    CodeNumber = 1,
                    CodeLength = codeLength,
                };
            }
            else
            {
                employeeCode = new EmployeeCode
                {
                    CodeNumber = employeeCodeLast.CodeNumber + 1,
                    CodeLength = codeLength,
                };
            }

            using (IDbContextTransaction transaction = _applicationDbContext.Database.BeginTransaction())
            {
                try
                {
                    await _applicationDbContext.EmployeeCodes.AddAsync(employeeCode);
                    await _applicationDbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new BadRequestException($"EmployeeCode create error\n{ex.ToMessage()}");
                }
            }
            return employeeCode;
        }

        public async Task<bool> EmployeesExistsAsync()
        {
            return await _applicationDbContext.Employees.AnyAsync();
        }

        public async Task DeleteEmployeeAsync(int id)
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

        public async Task DeleteEmployeesAsync(int[] ids)
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
    }
}