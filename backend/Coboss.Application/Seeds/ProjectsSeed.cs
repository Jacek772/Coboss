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
        private readonly IUsersService _usersService;

        public ProjectsSeed(IProjectsService projectsService,
            IEmployeesService employeesService,
            IUsersService usersService)
        {
            _projectsService = projectsService;
            _employeesService = employeesService;
            _usersService = usersService;
        }

        public async Task Seed()
        {
            if(await _projectsService.ExistsAsync())
            {
                return;
            }

            List<User> users = await _usersService.GetAsync();
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
                        Comments = new BusinnessTaskComment[]
                        {
                            new BusinnessTaskComment
                            {
                                Text = "Komentarz testowy 1",
                                Date = DateTime.Now.AddDays(2),
                                User = users.FirstOrDefault()
                            },
                            new BusinnessTaskComment
                            {
                                Text = "Komentarz testowy 2",
                                Date = DateTime.Now.AddDays(5),
                                User = users.FirstOrDefault()
                            }
                        },
                        TaskRealisations = new BusinnessTaskRealisation[]
                        {
                            new BusinnessTaskRealisation
                            {
                                Date = DateTime.Now,
                                Description = "Analiza wymagań",
                                TimeSpan = new TimeSpan(2,30,0),
                                Employee = employees.FirstOrDefault()
                            },
                            new BusinnessTaskRealisation
                            {
                                Date = DateTime.Now,
                                Description = "Tworzenie rozwiązania",
                                TimeSpan = new TimeSpan(4,0,0),
                                Employee = employees.FirstOrDefault()
                            }
                        }
                    },
                    new BusinnessTask
                    {
                        Name = "Task 2",
                        Date = DateTime.Now.AddDays(-2),
                        Description = "Vivamus sed pulvinar ex. Nullam felis sem.",
                        Term = DateTime.Now.AddDays(80),
                        Comments = new BusinnessTaskComment[]
                        {
                            new BusinnessTaskComment
                            {
                                Text = "Komentarz testowy 1",
                                Date = DateTime.Now.AddDays(2),
                                User = users.FirstOrDefault()
                            },
                            new BusinnessTaskComment
                            {
                                Text = "Komentarz testowy 2",
                                Date = DateTime.Now.AddDays(5),
                                User = users.FirstOrDefault()
                            }
                        },
                        TaskRealisations = new BusinnessTaskRealisation[]
                        {
                            new BusinnessTaskRealisation
                            {
                                Date = DateTime.Now,
                                Description = "Analiza wymagań",
                                TimeSpan = new TimeSpan(2,30,0),
                                Employee = employees.FirstOrDefault()
                            },
                            new BusinnessTaskRealisation
                            {
                                Date = DateTime.Now,
                                Description = "Tworzenie rozwiązania",
                                TimeSpan = new TimeSpan(4,0,0),
                                Employee = employees.FirstOrDefault()
                            }
                        }
                    },
                    new BusinnessTask
                    {
                        Name = "Task 3",
                        Date = DateTime.Now.AddDays(-10),
                        Description = "Interdum et malesuada fames ac ante ipsum primis in faucibus.",
                        Term = DateTime.Now.AddDays(10),
                        Comments = new BusinnessTaskComment[]
                        {
                            new BusinnessTaskComment
                            {
                                Text = "Komentarz testowy 1",
                                Date = DateTime.Now.AddDays(2),
                                User = users.FirstOrDefault()
                            },
                            new BusinnessTaskComment
                            {
                                Text = "Komentarz testowy 2",
                                Date = DateTime.Now.AddDays(5),
                                User = users.FirstOrDefault()
                            }
                        },
                        TaskRealisations = new BusinnessTaskRealisation[]
                        {
                            new BusinnessTaskRealisation
                            {
                                Date = DateTime.Now,
                                Description = "Analiza wymagań",
                                TimeSpan = new TimeSpan(2,30,0),
                                Employee = employees.FirstOrDefault()
                            },
                            new BusinnessTaskRealisation
                            {
                                Date = DateTime.Now,
                                Description = "Tworzenie rozwiązania",
                                TimeSpan = new TimeSpan(4,0,0),
                                Employee = employees.FirstOrDefault()
                            }
                        }
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
                        Comments = new BusinnessTaskComment[]
                        {
                            new BusinnessTaskComment
                            {
                                Text = "Komentarz testowy 1",
                                Date = DateTime.Now.AddDays(2),
                                User = users.FirstOrDefault()
                            },
                            new BusinnessTaskComment
                            {
                                Text = "Komentarz testowy 2",
                                Date = DateTime.Now.AddDays(5),
                                User = users.FirstOrDefault()
                            }
                        },
                        TaskRealisations = new BusinnessTaskRealisation[]
                        {
                            new BusinnessTaskRealisation
                            {
                                Date = DateTime.Now,
                                Description = "Analiza wymagań",
                                TimeSpan = new TimeSpan(2,30,0),
                                Employee = employees.FirstOrDefault()
                            },
                            new BusinnessTaskRealisation
                            {
                                Date = DateTime.Now,
                                Description = "Tworzenie rozwiązania",
                                TimeSpan = new TimeSpan(4,0,0),
                                Employee = employees.FirstOrDefault()
                            }
                        }
                    },
                    new BusinnessTask
                    {
                        Name = "Task 22",
                        Date = DateTime.Now.AddDays(-2),
                        Description = "Vivamus sed pulvinar ex. Nullam felis sem.",
                        Term = DateTime.Now.AddDays(80),
                        Comments = new BusinnessTaskComment[]
                        {
                            new BusinnessTaskComment
                            {
                                Text = "Komentarz testowy 1",
                                Date = DateTime.Now.AddDays(2),
                                User = users.FirstOrDefault()
                            },
                            new BusinnessTaskComment
                            {
                                Text = "Komentarz testowy 2",
                                Date = DateTime.Now.AddDays(5),
                                User = users.FirstOrDefault()
                            }
                        },
                        TaskRealisations = new BusinnessTaskRealisation[]
                        {
                            new BusinnessTaskRealisation
                            {
                                Date = DateTime.Now,
                                Description = "Analiza wymagań",
                                TimeSpan = new TimeSpan(2,30,0),
                                Employee = employees.FirstOrDefault()
                            },
                            new BusinnessTaskRealisation
                            {
                                Date = DateTime.Now,
                                Description = "Tworzenie rozwiązania",
                                TimeSpan = new TimeSpan(4,0,0),
                                Employee = employees.FirstOrDefault()
                            }
                        }
                    },
                    new BusinnessTask
                    {
                        Name = "Task 33",
                        Date = DateTime.Now.AddDays(-10),
                        Description = "Interdum et malesuada fames ac ante ipsum primis in faucibus.",
                        Term = DateTime.Now.AddDays(10),
                        Comments = new BusinnessTaskComment[]
                        {
                            new BusinnessTaskComment
                            {
                                Text = "Komentarz testowy 1",
                                Date = DateTime.Now.AddDays(2),
                                User = users.FirstOrDefault()
                            },
                            new BusinnessTaskComment
                            {
                                Text = "Komentarz testowy 2",
                                Date = DateTime.Now.AddDays(5),
                                User = users.FirstOrDefault()
                            }
                        },
                        TaskRealisations = new BusinnessTaskRealisation[]
                        {
                            new BusinnessTaskRealisation
                            {
                                Date = DateTime.Now,
                                Description = "Analiza wymagań",
                                TimeSpan = new TimeSpan(2,30,0),
                                Employee = employees.FirstOrDefault()
                            },
                            new BusinnessTaskRealisation
                            {
                                Date = DateTime.Now,
                                Description = "Tworzenie rozwiązania",
                                TimeSpan = new TimeSpan(4,0,0),
                                Employee = employees.FirstOrDefault()
                            }
                        }
                    },
                }
            };

            await _projectsService.CreateAsync(project2);
        }
    }
}
