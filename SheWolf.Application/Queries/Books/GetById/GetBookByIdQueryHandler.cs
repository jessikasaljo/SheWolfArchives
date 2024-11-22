using MediatR;
using SheWolf.Domain.Entities;
using SheWolf.Infrastructure.Database;

namespace SheWolf.Application.Queries.Books.GetById
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Book>
    {
        private readonly MockDatabase mockDatabase;

        public GetBookByIdQueryHandler(MockDatabase mockDatabase)
        {
            this.mockDatabase = mockDatabase;
        }

        public Task<Book> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            Book wantedBook = mockDatabase.books.FirstOrDefault(book => book.Id == request.Id)!;
            return Task.FromResult(wantedBook);
        }
    }
}
