using Microsoft.EntityFrameworkCore;
using SheWolf.Domain.Entities;

namespace SheWolf.Infrastructure.Database
{
    public class SheWolf_Database : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }

        public SheWolf_Database(DbContextOptions<SheWolf_Database> options) : base(options)
        {
        }
    }
}