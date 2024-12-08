using MediatR;
using SheWolf.Domain.Entities;
using SheWolf.Application.DTOs;

namespace SheWolf.Application.Commands.Authors.UpdateAuthor
{
    public class UpdateAuthorByIdCommand : IRequest<OperationResult<AuthorDto>>
    {
        public Author UpdatedAuthor { get; }
        public Guid Id { get; }

        public UpdateAuthorByIdCommand(Author updatedAuthor, Guid id)
        {
            UpdatedAuthor = updatedAuthor;
            Id = id;
        }
    }
}
