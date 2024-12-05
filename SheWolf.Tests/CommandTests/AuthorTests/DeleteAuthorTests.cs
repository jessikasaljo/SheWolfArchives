using Microsoft.EntityFrameworkCore;
using SheWolf.Application.Commands.Authors.DeleteAuthor;
using SheWolf.Application.Interfaces.RepositoryInterfaces;
using SheWolf.Domain.Entities;
using SheWolf.Infrastructure.Database;
using SheWolf.Infrastructure.Repositories;

namespace SheWolf.Tests.CommandTests.AuthorTests
{
    public class DeleteAuthorTests
    {
        private SheWolf_Database CreateInMemoryDatabase()
        {
            var options = new DbContextOptionsBuilder<SheWolf_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new SheWolf_Database(options);
        }

        [Fact]
        public async Task Handle_ShouldDeleteAuthorFromDatabase_WhenAuthorExists()
        {
            using var database = CreateInMemoryDatabase();
            var authorRepository = new AuthorRepository(database);
            var handler = new DeleteAuthorByIdCommandHandler(authorRepository);

            var existingAuthor = new Author { Id = Guid.NewGuid(), Name = "Author To Delete" };
            database.Authors.Add(existingAuthor);
            await database.SaveChangesAsync();

            var command = new DeleteAuthorByIdCommand(existingAuthor.Id);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.Equal("Successfully deleted author", result.Data);
            Assert.DoesNotContain(existingAuthor, database.Authors);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailureMessage_WhenAuthorDoesNotExist()
        {
            using var database = CreateInMemoryDatabase();
            var authorRepository = new AuthorRepository(database);
            var handler = new DeleteAuthorByIdCommandHandler(authorRepository);

            var nonExistentAuthorId = Guid.NewGuid();
            var command = new DeleteAuthorByIdCommand(nonExistentAuthorId);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.Equal("Failed to delete author", result.Data);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenRequestIsNull()
        {
            using var database = CreateInMemoryDatabase();
            var authorRepository = new AuthorRepository(database);
            var handler = new DeleteAuthorByIdCommandHandler(authorRepository);

            var result = await handler.Handle(null!, CancellationToken.None);

            Assert.False(result.Success);
            Assert.Equal("DeleteAuthorByIdCommand cannot be null.", result.ErrorMessage);
        }
    }
}
