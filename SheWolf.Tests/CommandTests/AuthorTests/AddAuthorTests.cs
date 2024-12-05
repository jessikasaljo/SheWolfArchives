using Microsoft.EntityFrameworkCore;
using SheWolf.Application.Commands.Authors.AddAuthor;
using SheWolf.Domain.Entities;
using SheWolf.Infrastructure.Database;
using SheWolf.Infrastructure.Repositories;

namespace SheWolf.Tests.CommandTests.AuthorTests
{
    public class AddAuthorTests
    {
        private SheWolf_Database CreateInMemoryDatabase()
        {
            var options = new DbContextOptionsBuilder<SheWolf_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new SheWolf_Database(options);
        }

        [Fact]
        public async Task Handle_ShouldAddAuthorToDatabase()
        {
            using var database = CreateInMemoryDatabase();
            var authorRepository = new AuthorRepository(database);
            var handler = new AddAuthorCommandHandler(authorRepository);

            var newAuthor = new Author
            {
                Name = "Simone de Beauvoir"
            };

            var command = new AddAuthorCommand(newAuthor);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(newAuthor.Name, result.Data.Name);
            Assert.NotEqual(Guid.Empty, result.Data.Id);

            var authorsInDatabase = await database.Authors.ToListAsync();
            Assert.Contains(authorsInDatabase, a => a.Id == result.Data.Id);
        }

        [Fact]
        public async Task Handle_ShouldCreateAuthorWithEmptyBooksList()
        {
            using var database = CreateInMemoryDatabase();
            var authorRepository = new AuthorRepository(database);
            var handler = new AddAuthorCommandHandler(authorRepository);

            var newAuthor = new Author
            {
                Name = "Sylvia Plath"
            };

            var command = new AddAuthorCommand(newAuthor);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Empty(result.Data.Books);

            var authorInDatabase = await database.Authors.FindAsync(result.Data.Id);
            Assert.NotNull(authorInDatabase);
            Assert.Empty(authorInDatabase.Books);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenNewAuthorIsNull()
        {
            using var database = CreateInMemoryDatabase();
            var authorRepository = new AuthorRepository(database);
            var handler = new AddAuthorCommandHandler(authorRepository);

            var command = new AddAuthorCommand(null!);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.False(result.Success);
            Assert.Equal("NewAuthor cannot be null.", result.ErrorMessage);
        }
    }
}
