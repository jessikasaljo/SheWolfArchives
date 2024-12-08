using FakeItEasy;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SheWolf.Application.DTOs;
using SheWolf.Application.Interfaces.RepositoryInterfaces;
using SheWolf.Domain.Entities;
using SheWolf.Infrastructure.Database;
using SheWolf.Infrastructure.Repositories;
using Xunit;

namespace SheWolf.Tests.IntegrationTests
{
    public class AuthorRepositoryTests : IDisposable
    {
        private readonly SheWolf_Database _database;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMediator _mediator;

        public AuthorRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<SheWolf_Database>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _database = new SheWolf_Database(options);
            _authorRepository = new AuthorRepository(_database);
            _mediator = A.Fake<IMediator>();
        }

        public void Dispose()
        {
            _database.Database.EnsureDeleted();
            _database.Dispose();
        }

        [Fact]
        public async Task AddAuthor_ShouldAddAuthor_WhenValidAuthorIsProvided()
        {
            var newAuthor = new Author { Id = Guid.NewGuid(), Name = "Chimamanda Ngozi Adichie" };
            var result = await _authorRepository.AddAuthor(newAuthor);
            Assert.NotNull(result);
            Assert.Equal("Chimamanda Ngozi Adichie", result.Name);
        }

        [Fact]
        public async Task GetAllAuthors_ShouldReturnAllAuthors_WhenCalled()
        {
            var author1 = new Author { Id = Guid.NewGuid(), Name = "Chimamanda Ngozi Adichie" };
            var author2 = new Author { Id = Guid.NewGuid(), Name = "Toni Morrison" };
            await _authorRepository.AddAuthor(author1);
            await _authorRepository.AddAuthor(author2);
            var result = await _authorRepository.GetAllAuthors();
            Assert.Equal(2, result.Count);
            Assert.Contains(result, a => a.Name == "Chimamanda Ngozi Adichie");
            Assert.Contains(result, a => a.Name == "Toni Morrison");
        }

        [Fact]
        public async Task GetAuthorById_ShouldReturnAuthor_WhenValidIdIsProvided()
        {
            var author = new Author { Id = Guid.NewGuid(), Name = "Chimamanda Ngozi Adichie" };
            await _authorRepository.AddAuthor(author);
            var result = await _authorRepository.GetAuthorById(author.Id);
            Assert.NotNull(result);
            Assert.Equal(author.Name, result.Name);
        }

        [Fact]
        public async Task GetAuthorById_ShouldThrowException_WhenInvalidIdIsProvided()
        {
            var invalidId = Guid.NewGuid();
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await _authorRepository.GetAuthorById(invalidId));

            Assert.StartsWith("Author with ID", exception.Message);
            Assert.Contains(invalidId.ToString(), exception.Message);
        }

        [Fact]
        public async Task UpdateAuthor_ShouldUpdateAuthor_WhenValidDataIsProvided()
        {
            var initialAuthor = new Author { Id = Guid.NewGuid(), Name = "Initial Author" };
            await _authorRepository.AddAuthor(initialAuthor);

            var updatedAuthorDto = new AuthorDto { Name = "Updated Author" };

            var updatedAuthor = await _authorRepository.UpdateAuthor(initialAuthor.Id, updatedAuthorDto);

            Assert.NotNull(updatedAuthor);
            Assert.Equal(updatedAuthorDto.Name, updatedAuthor.Name);
        }

        [Fact]
        public async Task DeleteAuthorById_ShouldDeleteAuthor_WhenValidIdIsProvided()
        {
            var author = new Author { Id = Guid.NewGuid(), Name = "Chimamanda Ngozi Adichie" };
            await _authorRepository.AddAuthor(author);
            var result = await _authorRepository.DeleteAuthorById(author.Id);
            Assert.Equal("Successfully deleted author", result);
        }

        [Fact]
        public async Task DeleteAuthorById_ShouldReturnFailure_WhenInvalidIdIsProvided()
        {
            var result = await _authorRepository.DeleteAuthorById(Guid.NewGuid());
            Assert.Equal("Failed to delete author", result);
        }
    }
}
