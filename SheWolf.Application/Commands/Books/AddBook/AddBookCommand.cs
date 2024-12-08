using MediatR;
using SheWolf.Domain.Entities;
using SheWolf.Application.DTOs;

namespace SheWolf.Application.Commands.Books.AddBook
{
    public class AddBookCommand : IRequest<OperationResult<BookDto>>
    {
        public Book NewBook { get; }

        public AddBookCommand(Book newBook)
        {
            NewBook = newBook;
        }
    }
}
