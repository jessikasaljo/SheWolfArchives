using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SheWolf.Infrastructure.Database;

namespace SheWolf.Infrastructure
{
    public class SheWolfDbContextFactory : IDesignTimeDbContextFactory<SheWolf_Database>
    {
        public SheWolf_Database CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "SheWolf.API"))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<SheWolf_Database>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new SheWolf_Database(optionsBuilder.Options);
        }
    }
}
