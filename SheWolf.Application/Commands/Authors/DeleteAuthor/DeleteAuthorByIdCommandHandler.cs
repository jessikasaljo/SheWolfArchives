using MediatR;
using SheWolf.Domain.Entities;
using SheWolf.Infrastructure.Database;

namespace SheWolf.Application.Commands.Authors.DeleteAuthor
{
    public class DeleteAuthorByIdCommandHandler : IRequestHandler<DeleteAuthorByIdCommand, Author>
    {
        private readonly MockDatabase mockDatabase;

        public DeleteAuthorByIdCommandHandler(MockDatabase mockDatabase)
        {
            this.mockDatabase = mockDatabase;
        }

        public Task<Author> Handle(DeleteAuthorByIdCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "DeleteAuthorByIdCommand cannot be null.");
            }

            Author? authorToDelete = mockDatabase.authors.FirstOrDefault(author => author.Id == request.Id);

            if (authorToDelete == null)
            {
                return Task.FromResult<Author>(null!);
            }

            mockDatabase.authors.Remove(authorToDelete);

            return Task.FromResult(authorToDelete);
        }
    }
}
