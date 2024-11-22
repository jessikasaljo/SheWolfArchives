using Microsoft.Extensions.DependencyInjection;
using SheWolf.Infrastructure.Database;

namespace SheWolf.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<MockDatabase>();
            return services;
        }
    }
}