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

            // Seeds
            services.AddScoped<UsersSeed>();

            // MediatR
            services.AddMediatR(x => x.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            // AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
