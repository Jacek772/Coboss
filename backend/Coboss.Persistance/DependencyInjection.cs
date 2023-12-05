using Microsoft.Extensions.DependencyInjection;

namespace Coboss.Persistance
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>();
            return services;
        }
    }
}
