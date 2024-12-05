using MediatR;
using SheWolf.Domain.Entities;

namespace SheWolf.Application.Commands.Authors.AddAuthor
{
    public class AddAuthorCommand : IRequest<OperationResult<Author>>
    {
        public Author NewAuthor { get; }

        public AddAuthorCommand(Author newAuthor)
        {
            NewAuthor = newAuthor;
        }
    }
}
