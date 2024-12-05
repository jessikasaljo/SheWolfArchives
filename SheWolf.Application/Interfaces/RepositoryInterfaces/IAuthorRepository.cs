using SheWolf.Domain.Entities;

namespace SheWolf.Application.Interfaces.RepositoryInterfaces
{
    public interface IAuthorRepository
    {
        Task<Author> AddAuthor(Author author);
        Task<List<Author>> GetAllAuthors();
        Task<Author> GetAuthorById(Guid id);
        Task<string> DeleteAuthorById(Guid id);
        Task<Author> UpdateAuthor(Guid id, Author author);

    }
}
