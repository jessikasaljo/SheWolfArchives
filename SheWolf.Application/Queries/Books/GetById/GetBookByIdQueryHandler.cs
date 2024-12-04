using MediatR;
using SheWolf.Domain.Entities;
using SheWolf.Application.Interfaces.RepositoryInterfaces;

namespace SheWolf.Application.Queries.Books.GetById
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Book>
    {
        private readonly IBookRepository _bookRepository;

        public GetBookByIdQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Book> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "GetBookByIdQuery cannot be null.");
            }

            Book wantedBook = await _bookRepository.GetBookById(request.Id);

            if (wantedBook == null)
            {
                throw new InvalidOperationException($"Book with ID {request.Id} not found.");
            }

            return wantedBook;
        }
    }
}