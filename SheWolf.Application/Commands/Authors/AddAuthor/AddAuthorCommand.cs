using MediatR;
using SheWolf.Domain.Entities;

namespace SheWolf.Application.Commands.Authors.AddAuthor
{
    public class AddAuthorCommand : IRequest<Author>
    {
        public AddAuthorCommand(Author newAuthor)
        {
            NewAuthor = newAuthor;
        }

        public Author NewAuthor { get; }
    }
}
