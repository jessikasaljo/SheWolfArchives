using Microsoft.EntityFrameworkCore;
using SheWolf.Application.Commands.Books.AddBook;
using SheWolf.Domain.Entities;
using SheWolf.Infrastructure.Database;
using SheWolf.Infrastructure.Repositories;

namespace SheWolf.Tests.CommandTests.BookTests
{
    public class AddBookTests
    {
        private SheWolf_Database CreateInMemoryDatabase()
        {
            var options = new DbContextOptionsBuilder<SheWolf_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new SheWolf_Database(options);
        }

        [Fact]
        public async Task Handle_ShouldAddBookToDatabase()
        {
            using var database = CreateInMemoryDatabase();
            var bookRepository = new BookRepository(database);
            var handler = new AddBookCommandHandler(bookRepository);

            var newBook = new Book
            {
                Title = "Rage becomes her: the power of women's anger"
            };

            var command = new AddBookCommand(newBook);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(newBook.Title, result.Data.Title);
            Assert.NotEqual(Guid.Empty, result.Data.Id);

            var booksInDatabase = await database.Books.ToListAsync();
            Assert.Contains(booksInDatabase, book => book.Title == newBook.Title);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenNewBookIsNull()
        {
            using var database = CreateInMemoryDatabase();
            var bookRepository = new BookRepository(database);
            var handler = new AddBookCommandHandler(bookRepository);

            AddBookCommand command = new(null);
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(command, CancellationToken.None));
            Assert.Equal("NewBook", exception.ParamName);
        }

        [Fact]
        public async Task Handle_ShouldCreateBookWithUniqueId()
        {
            using var database = CreateInMemoryDatabase();
            var bookRepository = new BookRepository(database);
            var handler = new AddBookCommandHandler(bookRepository);

            var newBook1 = new Book { Title = "The Idiot" };
            var newBook2 = new Book { Title = "You Exist Too Much" };

            var command1 = new AddBookCommand(newBook1);
            var command2 = new AddBookCommand(newBook2);

            var result1 = await handler.Handle(command1, CancellationToken.None);
            var result2 = await handler.Handle(command2, CancellationToken.None);

            Assert.NotEqual(result1.Data.Id, result2.Data.Id);
        }
    }
}
