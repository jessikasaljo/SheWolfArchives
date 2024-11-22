using MediatR;
using SheWolf.Domain.Entities;
using SheWolf.Infrastructure.Database;

namespace SheWolf.Application.Queries.Books.GetAll
{
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, List<Book>>
    {
        private readonly MockDatabase mockDatabase;

        public GetAllBooksQueryHandler(MockDatabase mockDatabase)
        {
            this.mockDatabase = mockDatabase;
        }

        public Task<List<Book>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            List<Book> allBooksFromMockDatabase = mockDatabase.books;
            return Task.FromResult(allBooksFromMockDatabase);
        }
    }
}
