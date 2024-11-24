using SheWolf.Application.Commands.Authors.UpdateAuthor;
using SheWolf.Domain.Entities;
using SheWolf.Infrastructure.Database;

namespace SheWolf.Tests.CommandTests.AuthorTests
{
    public class UpdateAuthorTests
    {
        [Fact]
        public async Task Handle_ShouldUpdateAuthorDetails()
        {
            var mockDatabase = new MockDatabase();
            var existingAuthor = new Author { Id = Guid.NewGuid(), Name = "Old Name" };
            mockDatabase.authors.Add(existingAuthor);

            var updatedAuthor = new Author { Name = "Updated Name" };
            var command = new UpdateAuthorByIdCommand(updatedAuthor, existingAuthor.Id);
            var handler = new UpdateAuthorByIdCommandHandler(mockDatabase);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(existingAuthor.Id, result.Id);
            Assert.Equal("Updated Name", result.Name);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenAuthorDoesNotExist()
        {
            var mockDatabase = new MockDatabase();
            var updatedAuthor = new Author { Name = "Updated Name" };
            var command = new UpdateAuthorByIdCommand(updatedAuthor, Guid.NewGuid());
            var handler = new UpdateAuthorByIdCommandHandler(mockDatabase);

            await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenUpdatedAuthorIsNull()
        {
            var mockDatabase = new MockDatabase();
            var command = new UpdateAuthorByIdCommand(null, Guid.NewGuid());
            var handler = new UpdateAuthorByIdCommandHandler(mockDatabase);

            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}
