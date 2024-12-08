using SheWolf.Application.DTOs;
using SheWolf.Domain.Entities;

namespace SheWolf.Application.Mappers
{
    public static class EntityMapper
    {
        public static AuthorDto MapToDto(Author author)
        {
            if (author == null) throw new ArgumentNullException(nameof(author));

            return new AuthorDto
            {
                Id = author.Id,
                Name = author.Name,
                BookTitles = author.Books?.Select(b => b.Title).ToList() ?? new List<string>()
            };
        }


        public static BookDto MapToDto(Book book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));

            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                AuthorId = book.AuthorId
            };
        }

        public static UserDto MapToDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Username = user.Username
            };
        }

        public static Author MapToEntity(AuthorDto authorDto)
        {
            if (authorDto == null) throw new ArgumentNullException(nameof(authorDto));

            return new Author
            {
                Id = authorDto.Id,
                Name = authorDto.Name
            };
        }

        public static Book MapToEntity(BookDto bookDto, Author author)
        {
            if (bookDto == null) throw new ArgumentNullException(nameof(bookDto));
            if (author == null) throw new ArgumentNullException(nameof(author));

            return new Book
            {
                Id = bookDto.Id,
                Title = bookDto.Title,
                AuthorId = bookDto.AuthorId,
                Author = author
            };
        }

        public static User MapToEntity(UserDto userDto)
        {
            return new User
            {
                Id = userDto.Id,
                Username = userDto.Username
            };
        }
    }
}
