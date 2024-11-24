using SheWolf.Application.Commands.Authors.DeleteAuthor;
using SheWolf.Domain.Entities;
using SheWolf.Infrastructure.Database;

namespace SheWolf.Tests.CommandTests.AuthorTests
{
    public class DeleteAuthorTests
    {
        [Fact]
        public async Task Handle_ShouldDeleteAuthorFromDatabase_WhenAuthorExists()
        {
            var mockDatabase = new MockDatabase();
            var existingAuthor = new Author { Id = Guid.NewGuid(), Name = "Author To Delete" };
            mockDatabase.authors.Add(existingAuthor);
            var handler = new DeleteAuthorByIdCommandHandler(mockDatabase);

            var command = new DeleteAuthorByIdCommand(existingAuthor.Id);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(existingAuthor.Id, result.Id);
            Assert.Equal(existingAuthor.Name, result.Name);
            Assert.DoesNotContain(result, mockDatabase.authors);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenAuthorDoesNotExist()
        {
            var mockDatabase = new MockDatabase();
            var handler = new DeleteAuthorByIdCommandHandler(mockDatabase);

            var nonExistentAuthorId = Guid.NewGuid();
            var command = new DeleteAuthorByIdCommand(nonExistentAuthorId);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.Null(result);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenRequestIsNull()
        {
            var mockDatabase = new MockDatabase();
            var handler = new DeleteAuthorByIdCommandHandler(mockDatabase);

            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null, CancellationToken.None));
        }
    }
}
