using Microsoft.EntityFrameworkCore;
using SheWolf.Application.Commands.Books.DeleteBook;
using SheWolf.Domain.Entities;
using SheWolf.Infrastructure.Database;
using SheWolf.Infrastructure.Repositories;
using Xunit;

namespace SheWolf.Tests.CommandTests.BookTests
{
    public class DeleteBookTests
    {
        private SheWolf_Database CreateInMemoryDatabase()
        {
            var options = new DbContextOptionsBuilder<SheWolf_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new SheWolf_Database(options);
        }

        [Fact]
        public async Task Handle_ShouldRemoveBookFromDatabase_WhenBookExists()
        {
            using var database = CreateInMemoryDatabase();
            var bookRepository = new BookRepository(database);
            var handler = new DeleteBookByIdCommandHandler(bookRepository);

            var existingBook = new Book
            {
                Id = Guid.NewGuid(),
                Title = "How to date men when you hate men"
            };

            database.Books.Add(existingBook);
            await database.SaveChangesAsync();

            var command = new DeleteBookByIdCommand(existingBook.Id);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.Equal("Successfully deleted book", result.Data);
            var booksInDatabase = await database.Books.ToListAsync();
            Assert.DoesNotContain(booksInDatabase, book => book.Id == existingBook.Id);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailureMessage_WhenBookDoesNotExist()
        {
            using var database = CreateInMemoryDatabase();
            var bookRepository = new BookRepository(database);
            var handler = new DeleteBookByIdCommandHandler(bookRepository);

            var nonExistentBookId = Guid.NewGuid();
            var command = new DeleteBookByIdCommand(nonExistentBookId);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.Equal("Failed to delete book", result.Data);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenRequestIsNull()
        {
            using var database = CreateInMemoryDatabase();
            var bookRepository = new BookRepository(database);
            var handler = new DeleteBookByIdCommandHandler(bookRepository);

            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null, CancellationToken.None));
        }
    }
}
