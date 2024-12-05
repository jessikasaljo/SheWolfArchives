using Microsoft.EntityFrameworkCore;
using SheWolf.Application.Commands.Authors.UpdateAuthor;
using SheWolf.Domain.Entities;
using SheWolf.Infrastructure.Database;
using SheWolf.Infrastructure.Repositories;

namespace SheWolf.Tests.CommandTests.AuthorTests
{
    public class UpdateAuthorTests
    {
        private SheWolf_Database CreateInMemoryDatabase()
        {
            var options = new DbContextOptionsBuilder<SheWolf_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new SheWolf_Database(options);
        }

        [Fact]
        public async Task Handle_ShouldUpdateAuthorDetails()
        {
            using var database = CreateInMemoryDatabase();
            var authorRepository = new AuthorRepository(database);
            var handler = new UpdateAuthorByIdCommandHandler(authorRepository);

            var existingAuthor = new Author { Id = Guid.NewGuid(), Name = "Old Name" };
            await database.Authors.AddAsync(existingAuthor);
            await database.SaveChangesAsync();

            var updatedAuthor = new Author { Name = "Updated Name" };
            var command = new UpdateAuthorByIdCommand(updatedAuthor, existingAuthor.Id);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(existingAuthor.Id, result.Data.Id);
            Assert.Equal("Updated Name", result.Data.Name);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenAuthorDoesNotExist()
        {
            using var database = CreateInMemoryDatabase();
            var authorRepository = new AuthorRepository(database);
            var handler = new UpdateAuthorByIdCommandHandler(authorRepository);

            var updatedAuthor = new Author { Name = "Updated Name" };
            var command = new UpdateAuthorByIdCommand(updatedAuthor, Guid.NewGuid());

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(command, CancellationToken.None));
            Assert.Equal("Failed to update author. No author found with Id: " + command.Id, exception.Message);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenUpdatedAuthorIsNull()
        {
            using var database = CreateInMemoryDatabase();
            var authorRepository = new AuthorRepository(database);
            var handler = new UpdateAuthorByIdCommandHandler(authorRepository);

            var command = new UpdateAuthorByIdCommand(null!, Guid.NewGuid());

            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}
