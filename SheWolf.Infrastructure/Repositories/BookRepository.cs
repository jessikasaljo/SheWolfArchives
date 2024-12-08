using Microsoft.EntityFrameworkCore;
using SheWolf.Application.Interfaces.RepositoryInterfaces;
using SheWolf.Domain.Entities;
using SheWolf.Infrastructure.Database;

namespace SheWolf.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly SheWolf_Database _database;

        public BookRepository(SheWolf_Database database)
        {
            _database = database;
        }

        public async Task<Book> AddBook(Book newBook)
        {
            if (newBook == null)
            {
                throw new ArgumentNullException(nameof(newBook), "The book cannot be null.");
            }

            var existingAuthor = _database.Authors.Local.FirstOrDefault(a => a.Id == newBook.Author.Id);
            if (existingAuthor == null)
            {
                if (_database.Entry(newBook.Author).State == EntityState.Detached)
                {
                    _database.Attach(newBook.Author);
                }
            }

            _database.Books.Add(newBook);
            await _database.SaveChangesAsync();

            return newBook;
        }



        public async Task<string> DeleteBookById(Guid id)
        {
            var bookToDelete = _database.Books.FirstOrDefault(book => book.Id == id);
            if (bookToDelete != null)
            {
                _database.Books.Remove(bookToDelete);
                await _database.SaveChangesAsync();
                return "Successfully deleted book";
            }
            else
            {
                return "Failed to delete book";
            }
        }

        public async Task<List<Book>> GetAllBooks()
        {
            try
            {
                var books = await _database.Books.ToListAsync();

                if (books == null || !books.Any())
                {
                    throw new Exception("No books found.");
                }

                return books;
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching books from database", ex);
            }
        }


        public async Task<Book> GetBookById(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Book ID cannot be an empty GUID.", nameof(id));
            }

            Book? book = await _database.Books.FirstOrDefaultAsync(book => book.Id == id);

            if (book == null)
            {
                throw new InvalidOperationException($"Book with ID {id} not found.");
            }

            return book;
        }

        public async Task<Book> UpdateBook(Guid id, Book bookToUpdate)
        {
            if (bookToUpdate == null)
            {
                throw new ArgumentNullException(nameof(bookToUpdate), "BookToUpdate cannot be null.");
            }

            var existingBook = _database.Books.FirstOrDefault(book => book.Id == id);
            if (existingBook == null)
            {
                return null;
            }

            existingBook.Title = bookToUpdate.Title;
            await _database.SaveChangesAsync();

            return existingBook;
        }
    }
}
