using MediatR;
using SheWolf.Application.Interfaces.RepositoryInterfaces;
using SheWolf.Domain.Entities;

namespace SheWolf.Application.Queries.Books.GetAll
{
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, OperationResult<List<Book>>>
    {
        private readonly IBookRepository _bookRepository;

        public GetAllBooksQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<OperationResult<List<Book>>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            List<Book> allBooks = await _bookRepository.GetAllBooks();

            if (allBooks == null || !allBooks.Any())
            {
                return OperationResult<List<Book>>.Failure("No books found in the system.");
            }

            return OperationResult<List<Book>>.Successful(allBooks);
        }
    }
}
