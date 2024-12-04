using MediatR;
using SheWolf.Domain.Entities;

namespace SheWolf.Application.Commands.Books.DeleteBook
{
    public class DeleteBookByIdCommand : IRequest<string>
    {
        public Guid Id { get; }

        public DeleteBookByIdCommand(Guid bookId)
        {
            Id = bookId;
        }
    }
}
