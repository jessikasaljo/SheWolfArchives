using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SheWolf.Infrastructure.Database;
using System.IO;

namespace SheWolf.Infrastructure
{
    public class SheWolfDbContextFactory : IDesignTimeDbContextFactory<SheWolf_Database>
    {
        public SheWolf_Database CreateDbContext(string[] args)
        {
            // Set the base path to the API project directory where appsettings.json is located
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "SheWolf.API")) // Move up to API directory
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Configure the DbContext with the connection string from appsettings.json
            var optionsBuilder = new DbContextOptionsBuilder<SheWolf_Database>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new SheWolf_Database(optionsBuilder.Options);
        }
    }
}
