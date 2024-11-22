using MediatR;
using SheWolf.Domain.Entities;
using SheWolf.Infrastructure.Database;

namespace SheWolf.Application.Queries.Authors.GetById
{
    public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, Author>
    {
        private readonly MockDatabase mockDatabase;

        public GetAuthorByIdQueryHandler(MockDatabase mockDatabase)
        {
            this.mockDatabase = mockDatabase;
        }

        public Task<Author> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            Author wantedAuthor = mockDatabase.authors.FirstOrDefault(author => author.Id == request.Id)!;
            return Task.FromResult(wantedAuthor);
        }
    }
}
