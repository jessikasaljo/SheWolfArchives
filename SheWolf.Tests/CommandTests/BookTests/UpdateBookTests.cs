using SheWolf.Application.Commands.Books.UpdateBook;
using SheWolf.Domain.Entities;
using SheWolf.Infrastructure.Database;

namespace SheWolf.Tests.CommandTests.BookTests
{
    public class UpdateBookTests
    {
        [Fact]
        public async Task Handle_ShouldUpdateBookTitle_WhenBookExists()
        {
            var mockDatabase = new MockDatabase();
            var existingBook = new Book { Title = "Old Title" };
            mockDatabase.books.Add(existingBook);

            var handler = new UpdateBookByIdCommandHandler(mockDatabase);
            var command = new UpdateBookByIdCommand(new Book { Title = "New Title" }, existingBook.Id);

            var updatedBook = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(updatedBook);
            Assert.Equal("New Title", updatedBook.Title);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenBookDoesNotExist()
        {
            var mockDatabase = new MockDatabase();
            var nonExistentBookId = Guid.NewGuid();

            var handler = new UpdateBookByIdCommandHandler(mockDatabase);
            var command = new UpdateBookByIdCommand(new Book { Title = "New Title" }, nonExistentBookId);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.Null(result);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenUpdatedBookIsNull()
        {
            var mockDatabase = new MockDatabase();
            var existingBook = new Book { Title = "Old Title" };
            mockDatabase.books.Add(existingBook);

            var handler = new UpdateBookByIdCommandHandler(mockDatabase);

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(
                () => handler.Handle(new UpdateBookByIdCommand(null!, existingBook.Id), CancellationToken.None)
            );

            Assert.Equal("UpdatedBook", exception.ParamName);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenRequestIsNull()
        {
            var mockDatabase = new MockDatabase();
            var handler = new UpdateBookByIdCommandHandler(mockDatabase);

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(
                () => handler.Handle(null!, CancellationToken.None)
            );

            Assert.Equal("request", exception.ParamName);
        }
    }
}
