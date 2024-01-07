using Bogus;
using Coboss.Application.Seeds.abstracts;
using Coboss.Application.Services.Abstracts;
using Coboss.Core.Entities;

namespace Coboss.Application.Seeds
{
    public class ProjectsSeed : ISeed
    {
        private readonly IProjectsService _projectsService;
        private readonly IEmployeesService _employeesService;

        public ProjectsSeed(IProjectsService projectsService,
            IEmployeesService employeesService)
        {
            _projectsService = projectsService;
            _employeesService = employeesService;
        }

        public async Task Seed()
        {
            if(await _projectsService.ExistsAsync())
            {
                return;
            }

            List<Employee> employees =  await _employeesService.GetAsync();

            Project project1 = new Project
            {
                Name = "Great company project",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis imperdiet nisl varius ullamcorper luctus. Quisque eu est dolor. In purus dolor, faucibus id orci quis, imperdiet interdum lorem. Suspendisse blandit velit ullamcorper magna finibus tempor. Morbi ante neque, pulvinar et sollicitudin eget, auctor vel mauris. Etiam nulla odio, aliquam at nisi in, aliquet viverra dui. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.",
                Term = DateTime.Now.AddDays(100),
                Manager = employees.LastOrDefault(),
                BusinnessTasks = new BusinnessTask[]
                {
                    new BusinnessTask
                    {
                        Name = "Task 1",
                        Date = DateTime.Now,
                        Description = "Ut quis felis egestas, accumsan velit ut, auctor sem. Nulla facilisi.",
                        Term = DateTime.Now.AddDays(30),
                    },
                    new BusinnessTask
                    {
                        Name = "Task 2",
                        Date = DateTime.Now.AddDays(-2),
                        Description = "Vivamus sed pulvinar ex. Nullam felis sem.",
                        Term = DateTime.Now.AddDays(80),
                    },
                    new BusinnessTask
                    {
                        Name = "Task 3",
                        Date = DateTime.Now.AddDays(-10),
                        Description = "Interdum et malesuada fames ac ante ipsum primis in faucibus.",
                        Term = DateTime.Now.AddDays(10),
                    },
                }
            };

            await _projectsService.CreateAsync(project1);

            Project project2 = new Project
            {
                Name = "Small company project",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis imperdiet nisl varius ullamcorper luctus. Quisque eu est dolor. In purus dolor, faucibus id orci quis, imperdiet interdum lorem. Suspendisse blandit velit ullamcorper magna finibus tempor. Morbi ante neque, pulvinar et sollicitudin eget, auctor vel mauris. Etiam nulla odio, aliquam at nisi in, aliquet viverra dui. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.",
                Term = DateTime.Now.AddDays(100),
                Manager = employees.LastOrDefault(),
                BusinnessTasks = new BusinnessTask[]
                {
                    new BusinnessTask
                    {
                        Name = "Task 11",
                        Date = DateTime.Now,
                        Description = "Ut quis felis egestas, accumsan velit ut, auctor sem. Nulla facilisi.",
                        Term = DateTime.Now.AddDays(30),
                    },
                    new BusinnessTask
                    {
                        Name = "Task 22",
                        Date = DateTime.Now.AddDays(-2),
                        Description = "Vivamus sed pulvinar ex. Nullam felis sem.",
                        Term = DateTime.Now.AddDays(80),
                    },
                    new BusinnessTask
                    {
                        Name = "Task 33",
                        Date = DateTime.Now.AddDays(-10),
                        Description = "Interdum et malesuada fames ac ante ipsum primis in faucibus.",
                        Term = DateTime.Now.AddDays(10),
                    },
                }
            };

            await _projectsService.CreateAsync(project2);
        }
    }
}
