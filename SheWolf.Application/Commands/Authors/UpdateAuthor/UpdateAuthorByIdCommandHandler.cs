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
            Author authorToUpdate = mockDatabase.authors.FirstOrDefault(author => author.Id == request.Id)!;

            authorToUpdate.Name = request.UpdatedAuthor.Name;

            return Task.FromResult(authorToUpdate);
        }
    }
}
