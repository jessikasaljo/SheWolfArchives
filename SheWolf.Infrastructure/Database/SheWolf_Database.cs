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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasOne(b => b.Author)
                      .WithMany(a => a.Books)
                      .HasForeignKey(b => b.AuthorId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.Property(b => b.Title)
                      .IsRequired()
                      .HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.Username)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(u => u.Password)
                      .IsRequired()
                      .HasMaxLength(255);
            });
        }


    }
}