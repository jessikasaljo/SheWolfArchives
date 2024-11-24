using SheWolf.Application.Commands.Books.AddBook;
using SheWolf.Domain.Entities;
using SheWolf.Infrastructure.Database;

namespace SheWolf.Tests.CommandTests.BookTests
{
    public class AddBookTests
    {
        [Fact]
        public async Task Handle_ShouldAddBookToDatabase()
        {
            var mockDatabase = new MockDatabase();
            var handler = new AddBookCommandHandler(mockDatabase);

            var newBook = new Book
            {
                Title = "Rage becomes her: the power of women's anger"
            };

            var command = new AddBookCommand(newBook);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(newBook.Title, result.Title);
            Assert.NotEqual(Guid.Empty, result.Id);
            Assert.Contains(result, mockDatabase.books);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenNewBookIsNull()
        {
            var mockDatabase = new MockDatabase();
            var handler = new AddBookCommandHandler(mockDatabase);

            AddBookCommand command = new(null);

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(command, CancellationToken.None));
            Assert.Equal("NewBook", exception.ParamName);
        }

        [Fact]
        public async Task Handle_ShouldCreateBookWithUniqueId()
        {
            var mockDatabase = new MockDatabase();
            var handler = new AddBookCommandHandler(mockDatabase);

            var newBook1 = new Book { Title = "The Idiot" };
            var newBook2 = new Book { Title = "You exist too much" };

            var command1 = new AddBookCommand(newBook1);
            var command2 = new AddBookCommand(newBook2);

            var result1 = await handler.Handle(command1, CancellationToken.None);
            var result2 = await handler.Handle(command2, CancellationToken.None);

            Assert.NotEqual(result1.Id, result2.Id);
        }
    }
}
