using MediatR;
using SheWolf.Domain.Entities;
using SheWolf.Infrastructure.Database;

namespace SheWolf.Application.Commands.Authors.AddAuthor
{
    public class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand, Author>
    {
        private readonly MockDatabase mockDatabase;

        public AddAuthorCommandHandler(MockDatabase mockDatabase)
        {
            this.mockDatabase = mockDatabase;
        }

        public Task<Author> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "AddAuthorCommand cannot be null.");
            }

            if (request.NewAuthor == null)
            {
                throw new ArgumentNullException(nameof(request.NewAuthor), "NewAuthor cannot be null.");
            }

            Author authorToCreate = new()
            {
                Id = Guid.NewGuid(),
                Name = request.NewAuthor.Name,
                Books = new List<Book>()
            };

            mockDatabase.authors.Add(authorToCreate);

            return Task.FromResult(authorToCreate);
        }
    }
}
