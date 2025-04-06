using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SheWolf.Application.Interfaces.RepositoryInterfaces;
using SheWolf.Infrastructure.Database;
using SheWolf.Infrastructure.Repositories;

namespace SheWolf.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<SheWolf_Database>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}