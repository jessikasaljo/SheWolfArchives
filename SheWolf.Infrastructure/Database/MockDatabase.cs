using SheWolf.Domain.Entities;

namespace SheWolf.Infrastructure.Database
{
    public class MockDatabase
    {
        public List<Book> books { get; private set; }
        public List<Author> authors { get; private set; }
        public List<User> users { get; private set; }

        public MockDatabase()
        {

            var author1 = new Author { Id = Guid.NewGuid(), Name = "Pauline Harmange", Books = new List<Book>() };
            var author2 = new Author { Id = Guid.NewGuid(), Name = "Allison Kelley", Books = new List<Book>() };
            var author3 = new Author { Id = Guid.NewGuid(), Name = "Katy Brent", Books = new List<Book>() };
            var author4 = new Author { Id = Guid.NewGuid(), Name = "François Camoin", Books = new List<Book>() };

            var book1 = new Book { Id = Guid.NewGuid(), Title = "I hate men", Author = author1 };
            var book2 = new Book { Id = Guid.NewGuid(), Title = "Jokes to offend men", Author = author2 };
            var book3 = new Book { Id = Guid.NewGuid(), Title = "How to kill men and get away with it", Author = author3 };
            var book4 = new Book { Id = Guid.NewGuid(), Title = "Why men are afraid of women", Author = author4 };

            var user1 = new User { Id = Guid.NewGuid(), Username = "jessika" };
            var user2 = new User { Id = Guid.NewGuid(), Username = "elin" };
            var user3 = new User { Id = Guid.NewGuid(), Username = "johanna" };

            author1.Books.Add(book1);
            author2.Books.Add(book2);
            author3.Books.Add(book3);
            author4.Books.Add(book4);

            authors = new List<Author> { author1, author2, author3, author4 };
            books = new List<Book> { book1, book2, book3, book4 };
            users = new List<User> { user1, user2, user3 };
        }
    }
}
