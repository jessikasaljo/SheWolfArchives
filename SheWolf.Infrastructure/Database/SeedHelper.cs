using SheWolf.Application.Queries.Books.GetAll;
using SheWolf.Domain.Entities;

namespace SheWolf.Infrastructure.Database
{
    public static class SeedHelper
    {
        public static void Seed(SheWolf_Database db)
        {
            if (!db.Authors.Any())
            {
                var author1 = new Author { Id = Guid.NewGuid(), Name = "Pauline Harmange", Books = new List<Book>() };
                var author2 = new Author { Id = Guid.NewGuid(), Name = "Allison Kelley", Books = new List<Book>() };
                var author3 = new Author { Id = Guid.NewGuid(), Name = "Katy Brent", Books = new List<Book>() };
                var author4 = new Author { Id = Guid.NewGuid(), Name = "François Camoin", Books = new List<Book>() };

                db.Authors.AddRange(author1, author2, author3, author4);
                db.SaveChanges();
            }

            if (!db.Books.Any())
            {
                var author1 = db.Authors.FirstOrDefault(a => a.Name == "Pauline Harmange");
                var author2 = db.Authors.FirstOrDefault(a => a.Name == "Allison Kelley");
                var author3 = db.Authors.FirstOrDefault(a => a.Name == "Katy Brent");
                var author4 = db.Authors.FirstOrDefault(a => a.Name == "François Camoin");

                var book1 = new Book { Id = Guid.NewGuid(), Title = "I hate men", Author = author1, Picture = "book_covers/howtokillmen.png" };
                var book2 = new Book { Id = Guid.NewGuid(), Title = "Jokes to offend men", Author = author2 };
                var book3 = new Book { Id = Guid.NewGuid(), Title = "How to kill men and get away with it", Author = author3 };
                var book4 = new Book { Id = Guid.NewGuid(), Title = "Why men are afraid of women", Author = author4 };

                db.Books.AddRange(book1, book2, book3, book4);
                db.SaveChanges();
            }

            if (!db.Users.Any())
            {
                var user1 = new User { Id = Guid.NewGuid(), Username = "jessika" };
                var user2 = new User { Id = Guid.NewGuid(), Username = "elin" };
                var user3 = new User { Id = Guid.NewGuid(), Username = "johanna" };

                db.Users.AddRange(user1, user2, user3);
                db.SaveChanges();
            }
        }
    }
}
