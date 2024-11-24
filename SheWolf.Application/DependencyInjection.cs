using Microsoft.Extensions.DependencyInjection;
using SheWolf.Application.Queries.Users.Login.Helpers;

namespace SheWolf.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;
            services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));

            services.AddScoped<TokenHelper>();

            return services;
        }
    }
}
