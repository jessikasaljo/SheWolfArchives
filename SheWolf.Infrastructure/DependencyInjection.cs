using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SheWolf.Infrastructure.Database;

namespace SheWolf.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<MockDatabase>();

            services.AddDbContext<SheWolf_Database>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            return services;
        }
    }
}