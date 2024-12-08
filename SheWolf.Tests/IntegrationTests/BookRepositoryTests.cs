using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using SheWolf.Domain.Entities;
using SheWolf.Infrastructure.Database;
using SheWolf.Infrastructure.Repositories;
using Xunit;

namespace SheWolf.Tests.IntegrationTests
{
    public class BookRepositoryTests : IDisposable
    {
        private readonly SheWolf_Database _database;
        private readonly BookRepository _bookRepository;

        public BookRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<SheWolf_Database>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _database = new SheWolf_Database(options);
            _bookRepository = new BookRepository(_database);
        }

        public void Dispose()
        {
            _database.Database.EnsureDeleted();
            _database.Dispose();
        }

        [Fact]
        public async Task AddBook_ShouldAddBook_WhenValidDataIsProvided()
        {
            var newAuthor = new Author
            {
                Id = Guid.NewGuid(),
                Name = "Jane Doe"
            };

            _database.Authors.Add(newAuthor);
            await _database.SaveChangesAsync();

            var newBook = new Book
            {
                Title = "The Feminist Manifesto",
                AuthorId = newAuthor.Id,
                Author = newAuthor
            };

            var addedBook = await _bookRepository.AddBook(newBook);

            Assert.NotNull(addedBook);
            Assert.Equal(newBook.Title, addedBook.Title);
            Assert.Equal(newBook.AuthorId, addedBook.AuthorId);
            Assert.Equal(newBook.Author.Id, addedBook.Author.Id);
        }

        [Fact]
        public async Task GetBookById_ShouldReturnBook_WhenValidIdIsProvided()
        {
            var author = new Author { Id = Guid.NewGuid(), Name = "Alice Walker" };
            _database.Authors.Add(author);
            await _database.SaveChangesAsync();

            var newBook = new Book { Title = "The Color Purple", AuthorId = author.Id };
            _database.Books.Add(newBook);
            await _database.SaveChangesAsync();

            var book = await _bookRepository.GetBookById(newBook.Id);

            Assert.NotNull(book);
            Assert.Equal(newBook.Id, book.Id);
            Assert.Equal(newBook.Title, book.Title);
            Assert.Equal(newBook.AuthorId, book.AuthorId);
        }

        [Fact]
        public async Task GetBookById_ShouldThrowException_WhenInvalidIdIsProvided()
        {
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await _bookRepository.GetBookById(Guid.NewGuid()));

            Assert.StartsWith("Book with ID", exception.Message);
        }

        [Fact]
        public async Task UpdateBook_ShouldUpdateBook_WhenValidDataIsProvided()
        {
            var author = new Author { Id = Guid.NewGuid(), Name = "Maya Angelou" };
            _database.Authors.Add(author);
            await _database.SaveChangesAsync();

            var newBook = new Book { Title = "I Know Why the Caged Bird Sings", AuthorId = author.Id };
            _database.Books.Add(newBook);
            await _database.SaveChangesAsync();

            var updatedBook = new Book { Title = "The Caged Bird's Song", AuthorId = author.Id };

            var book = await _bookRepository.UpdateBook(newBook.Id, updatedBook);

            Assert.NotNull(book);
            Assert.Equal(updatedBook.Title, book.Title);
            Assert.Equal(updatedBook.AuthorId, book.AuthorId);
        }

        [Fact]
        public async Task DeleteBookById_ShouldDeleteBook_WhenValidIdIsProvided()
        {
            var author = new Author { Id = Guid.NewGuid(), Name = "Chimamanda Ngozi Adichie" };
            _database.Authors.Add(author);
            await _database.SaveChangesAsync();

            var newBook = new Book { Title = "We Should All Be Feminists", AuthorId = author.Id };
            _database.Books.Add(newBook);
            await _database.SaveChangesAsync();

            var result = await _bookRepository.DeleteBookById(newBook.Id);

            Assert.Equal("Successfully deleted book", result);
        }

        [Fact]
        public async Task DeleteBookById_ShouldReturnFailureMessage_WhenInvalidIdIsProvided()
        {
            var result = await _bookRepository.DeleteBookById(Guid.NewGuid());

            Assert.Equal("Failed to delete book", result);
        }

        [Fact]
        public async Task GetAllBooks_ShouldReturnAllBooks_WhenCalled()
        {
            var author = new Author { Id = Guid.NewGuid(), Name = "Zadie Smith" };
            _database.Authors.Add(author);
            await _database.SaveChangesAsync();

            var book1 = new Book { Title = "On Beauty", AuthorId = author.Id };
            var book2 = new Book { Title = "White Teeth", AuthorId = author.Id };
            _database.Books.AddRange(book1, book2);
            await _database.SaveChangesAsync();

            var books = await _bookRepository.GetAllBooks();

            Assert.NotNull(books);
            Assert.Equal(2, books.Count);
        }
    }
}
