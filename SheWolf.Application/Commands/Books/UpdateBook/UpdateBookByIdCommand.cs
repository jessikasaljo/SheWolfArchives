using MediatR;
using SheWolf.Domain.Entities;
using SheWolf.Application.DTOs;

namespace SheWolf.Application.Commands.Books.UpdateBook
{
    public class UpdateBookByIdCommand : IRequest<OperationResult<BookDto>>
    {
        public Book UpdatedBook { get; }
        public Guid Id { get; }

        public UpdateBookByIdCommand(Book updatedBook, Guid id)
        {
            UpdatedBook = updatedBook;
            Id = id;
        }
    }
}
