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
            services.AddScoped<IPasswordHasherService, PasswordHasherService>();
            services.AddScoped<IAuthService, AuthService>();

            // MediatR
            services.AddMediatR(x => x.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            // AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
