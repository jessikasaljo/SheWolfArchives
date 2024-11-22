using MediatR;
using SheWolf.Domain.Entities;

namespace SheWolf.Application.Commands.Authors.DeleteAuthor
{
    public class DeleteAuthorByIdCommand : IRequest<Author>
    {
        public Guid Id { get; }

        public DeleteAuthorByIdCommand(Guid authorId)
        {
            Id = authorId;
        }
    }
}
