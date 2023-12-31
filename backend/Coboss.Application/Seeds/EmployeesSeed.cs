using Bogus;
using Bogus.Extensions;
using Bogus.Extensions.Poland;
using Coboss.Application.Seeds.abstracts;
using Coboss.Application.Services.Abstracts;
using Coboss.Core.Entities;

namespace Coboss.Application.Seeds
{
    public class EmployeesSeed : ISeed
    {
        private readonly IEmployeesService _employeesService;

        public EmployeesSeed(IEmployeesService employeesService)
        {
            _employeesService = employeesService;
        }

        public async Task Seed()
        {
            if(await _employeesService.EmployeesExistsAsync())
            {
                return;
            }

            Faker<Employee> employeesGenerator = new Faker<Employee>("pl")
                 .RuleFor(a => a.Name, f => f.Person.FirstName.ClampLength(max: 50))
                 .RuleFor(a => a.Surname, f => f.Person.LastName.ClampLength(max: 100))
                 .RuleFor(a => a.PESEL, f => f.Person.Pesel())
                 .RuleFor(a => a.DateOfBirth, f => f.Person.DateOfBirth);

            Faker<EmployeeHistory> employeeHistoriesGenerator = new Faker<EmployeeHistory>("pl")
                .RuleFor(a => a.DateFrom, f => f.Date.Past())
                .RuleFor(a => a.DateTo, f => f.Date.Future())
                .RuleFor(a => a.CostHourOfWork, f => f.Finance.Amount(20, 100));

            IEnumerable<Employee> employees = employeesGenerator.Generate(100);
            foreach (Employee employee in employees)
            {
                EmployeeHistory employeeHistory = employeeHistoriesGenerator.Generate(1).FirstOrDefault();
                employee.EmployeeHistories = new List<EmployeeHistory> { employeeHistory };
            }

            await _employeesService.CreateEmployeesAsync(employees);
        }
    }
}