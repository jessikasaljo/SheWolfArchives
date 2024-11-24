using MediatR;
using SheWolf.Domain.Entities;
using SheWolf.Infrastructure.Database;

namespace SheWolf.Application.Commands.Authors.UpdateAuthor
{
    public class UpdateAuthorByIdCommandHandler : IRequestHandler<UpdateAuthorByIdCommand, Author>
    {
        private readonly MockDatabase mockDatabase;

        public UpdateAuthorByIdCommandHandler(MockDatabase mockDatabase)
        {
            this.mockDatabase = mockDatabase;
        }

        public Task<Author> Handle(UpdateAuthorByIdCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "UpdateAuthorByIdCommand cannot be null.");
            }

            if (request.UpdatedAuthor == null)
            {
                throw new ArgumentNullException(nameof(request.UpdatedAuthor), "UpdatedAuthor cannot be null.");
            }

            var authorToUpdate = mockDatabase.authors.FirstOrDefault(author => author.Id == request.Id);

            if (authorToUpdate == null)
            {
                throw new InvalidOperationException($"No author found with Id: {request.Id}");
            }

            authorToUpdate.Name = request.UpdatedAuthor.Name;

            return Task.FromResult(authorToUpdate);
        }
    }
}
