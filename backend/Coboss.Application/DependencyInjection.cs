using Coboss.Application.Seeds;
using Coboss.Application.Services;
using Coboss.Application.Services.Abstracts;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Coboss.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IPasswordHasherService, PasswordHasherService>();
            services.AddScoped<IRolesService, RolesService>();
            services.AddScoped<IEmployeesService, EmployeesService>();
            services.AddScoped<IProjectsService, ProjectsService>();
            services.AddScoped<IGlobalSettingsService, GlobalSettingsService>();
            services.AddScoped<IObjectCodesService, ObjectCodesService>();
            services.AddScoped<IBusinnessTasksService, BusinnessTasksService>();
            services.AddScoped<IBusinnessTaskRealisationsService, BusinnessTaskRealisationsService>();
            services.AddScoped<IBusinnessTaskCommentsService, BusinnessTaskCommentsService>();

            // Seeds
            services.AddScoped<UsersSeed>();
            services.AddScoped<RolesSeed>();
            services.AddScoped<EmployeesSeed>();
            services.AddScoped<GlobalSettingsSeed>();
            services.AddScoped<ProjectsSeed>();

            // MediatR
            services.AddMediatR(x => x.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            // AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
