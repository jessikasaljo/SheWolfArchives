using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SheWolf.Application.Interfaces.RepositoryInterfaces;
using SheWolf.Infrastructure.Database;
using SheWolf.Infrastructure.Repository;

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

            services.AddScoped<IAuthorRepository, AuthorRepository>();

            return services;
        }
    }
}