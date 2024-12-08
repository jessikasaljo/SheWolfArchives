using MediatR;
using SheWolf.Domain.Entities;
using SheWolf.Application.DTOs;

namespace SheWolf.Application.Commands.Authors.AddAuthor
{
    public class AddAuthorCommand : IRequest<OperationResult<AuthorDto>>
    {
        public Author NewAuthor { get; }

        public AddAuthorCommand(Author newAuthor)
        {
            NewAuthor = newAuthor;
        }
    }
}
