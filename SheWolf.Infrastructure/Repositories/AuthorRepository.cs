using Microsoft.EntityFrameworkCore;
using SheWolf.Application.DTOs;
using SheWolf.Application.Interfaces.RepositoryInterfaces;
using SheWolf.Domain.Entities;
using SheWolf.Infrastructure.Database;

namespace SheWolf.Infrastructure.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly SheWolf_Database _database;

        public AuthorRepository(SheWolf_Database database)
        {
            _database = database;
        }

        public async Task<Author> AddAuthor(Author newAuthor)
        {
            _database.Authors.Add(newAuthor);
            await _database.SaveChangesAsync();
            return newAuthor;
        }

        public async Task<string> DeleteAuthorById(Guid id)
        {
            var authorToDelete = _database.Authors.FirstOrDefault(author => author.Id == id);
            if (authorToDelete != null)
            {
                _database.Authors.Remove(authorToDelete);
                await _database.SaveChangesAsync();
                return "Successfully deleted author";
            }
            else
            {
                return "Failed to delete author";
            }
        }

        public async Task<List<Author>> GetAllAuthors()
        {
            return await _database.Authors
                .Include(a => a.Books)
                .ToListAsync();
        }

        public async Task<Author> GetAuthorById(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Author ID cannot be an empty GUID.", nameof(id));
            }

            Author? author = await _database.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (author == null)
            {
                throw new InvalidOperationException($"Author with ID {id} not found.");
            }

            return author;
        }


        public async Task<Author> UpdateAuthor(Guid id, AuthorDto authorToUpdateDto)
        {
            if (authorToUpdateDto == null)
            {
                throw new ArgumentNullException(nameof(authorToUpdateDto), "AuthorToUpdate cannot be null.");
            }

            var existingAuthor = _database.Authors.FirstOrDefault(author => author.Id == id);
            if (existingAuthor == null)
            {
                return null;
            }

            existingAuthor.Name = authorToUpdateDto.Name;
            await _database.SaveChangesAsync();

            return existingAuthor;
        }
    }
}
