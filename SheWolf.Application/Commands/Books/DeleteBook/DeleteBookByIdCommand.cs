using MediatR;
using SheWolf.Domain.Entities;

namespace SheWolf.Application.Commands.Books.DeleteBook
{
    public class DeleteBookByIdCommand : IRequest<Book>
    {
        public Guid Id { get; }

        public DeleteBookByIdCommand(Guid bookId)
        {
            Id = bookId;
        }
    }
}
