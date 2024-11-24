using SheWolf.Application.Commands.Authors.AddAuthor;
using SheWolf.Domain.Entities;
using SheWolf.Infrastructure.Database;

namespace SheWolf.Tests.CommandTests.AuthorTests
{
    public class AddAuthorTests
    {
        [Fact]
        public async Task Handle_ShouldAddAuthorToDatabase()
        {
            var mockDatabase = new MockDatabase();
            var handler = new AddAuthorCommandHandler(mockDatabase);

            var newAuthor = new Author
            {
                Name = "Simone de Beauvoir"
            };

            var command = new AddAuthorCommand(newAuthor);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(newAuthor.Name, result.Name);
            Assert.NotEqual(Guid.Empty, result.Id);
            Assert.Contains(result, mockDatabase.authors);
        }

        [Fact]
        public async Task Handle_ShouldCreateAuthorWithEmptyBooksList()
        {
            var mockDatabase = new MockDatabase();
            var handler = new AddAuthorCommandHandler(mockDatabase);

            var newAuthor = new Author
            {
                Name = "Sylvia Plath"
            };

            var command = new AddAuthorCommand(newAuthor);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Empty(result.Books);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenNewAuthorIsNull()
        {
            var mockDatabase = new MockDatabase();
            var handler = new AddAuthorCommandHandler(mockDatabase);

            AddAuthorCommand command = new(null);

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(command, CancellationToken.None));
            Assert.Equal("NewAuthor", exception.ParamName);
        }
    }
}