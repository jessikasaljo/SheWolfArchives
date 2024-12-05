using Microsoft.EntityFrameworkCore;
using SheWolf.Application.Commands.Books.UpdateBook;
using SheWolf.Domain.Entities;
using SheWolf.Infrastructure.Database;
using SheWolf.Infrastructure.Repositories;
using Xunit;

namespace SheWolf.Tests.CommandTests.BookTests
{
    public class UpdateBookTests
    {
        private SheWolf_Database CreateInMemoryDatabase()
        {
            var options = new DbContextOptionsBuilder<SheWolf_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new SheWolf_Database(options);
        }

        [Fact]
        public async Task Handle_ShouldUpdateBookTitle_WhenBookExists()
        {
            using var database = CreateInMemoryDatabase();
            var bookRepository = new BookRepository(database);
            var handler = new UpdateBookByIdCommandHandler(bookRepository);

            var existingBook = new Book { Id = Guid.NewGuid(), Title = "Old Title" };
            database.Books.Add(existingBook);
            await database.SaveChangesAsync();

            var command = new UpdateBookByIdCommand(new Book { Title = "New Title" }, existingBook.Id);

            var updatedBook = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(updatedBook);
            Assert.Equal("New Title", updatedBook.Data.Title);

            var bookInDatabase = await database.Books.FindAsync(existingBook.Id);
            Assert.NotNull(bookInDatabase);
            Assert.Equal("New Title", bookInDatabase.Title);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenBookDoesNotExist()
        {
            using var database = CreateInMemoryDatabase();
            var bookRepository = new BookRepository(database);
            var handler = new UpdateBookByIdCommandHandler(bookRepository);

            var nonExistentBookId = Guid.NewGuid();
            var command = new UpdateBookByIdCommand(new Book { Title = "New Title" }, nonExistentBookId);

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(
                () => handler.Handle(command, CancellationToken.None)
            );

            Assert.Equal($"Failed to update book. No book found with Id: {nonExistentBookId}", exception.Message);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenUpdatedBookIsNull()
        {
            using var database = CreateInMemoryDatabase();
            var bookRepository = new BookRepository(database);
            var handler = new UpdateBookByIdCommandHandler(bookRepository);

            var existingBook = new Book { Id = Guid.NewGuid(), Title = "Old Title" };
            database.Books.Add(existingBook);
            await database.SaveChangesAsync();

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(
                () => handler.Handle(new UpdateBookByIdCommand(null!, existingBook.Id), CancellationToken.None)
            );

            Assert.Equal("UpdatedBook", exception.ParamName);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenRequestIsNull()
        {
            using var database = CreateInMemoryDatabase();
            var bookRepository = new BookRepository(database);
            var handler = new UpdateBookByIdCommandHandler(bookRepository);

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(
                () => handler.Handle(null!, CancellationToken.None)
            );

            Assert.Equal("request", exception.ParamName);
        }
    }
}
