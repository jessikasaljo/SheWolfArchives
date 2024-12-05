using SheWolf.Domain.Entities;

namespace SheWolf.Application.Interfaces.RepositoryInterfaces
{
    public interface IBookRepository
    {
        Task<Book> AddBook(Book book);
        Task<List<Book>> GetAllBooks();
        Task<Book> GetBookById(Guid id);
        Task<string> DeleteBookById(Guid id);
        Task<Book> UpdateBook(Guid id, Book book);

    }
}