using SheWolf.Application.Commands.Books.DeleteBook;
using SheWolf.Domain.Entities;
using SheWolf.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheWolf.Tests.CommandTests.BookTests
{
    public class DeleteBookTests
    {
        [Fact]
        public async Task Handle_ShouldRemoveBookFromDatabase_WhenBookExists()
        {
            var mockDatabase = new MockDatabase();
            var handler = new DeleteBookByIdCommandHandler(mockDatabase);

            var existingBook = new Book
            {
                Id = Guid.NewGuid(),
                Title = "How to date men when you hate men"
            };
            mockDatabase.books.Add(existingBook);

            var command = new DeleteBookByIdCommand(existingBook.Id);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(existingBook.Id, result.Id);
            Assert.DoesNotContain(result, mockDatabase.books);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenBookDoesNotExist()
        {
            var mockDatabase = new MockDatabase();
            var handler = new DeleteBookByIdCommandHandler(mockDatabase);

            var nonExistentBookId = Guid.NewGuid();
            var command = new DeleteBookByIdCommand(nonExistentBookId);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.Null(result);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenRequestIsNull()
        {
            var mockDatabase = new MockDatabase();
            var handler = new DeleteBookByIdCommandHandler(mockDatabase);

            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null, CancellationToken.None));
        }
    }
}
