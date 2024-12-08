using MediatR;
using Microsoft.Extensions.Caching.Memory;
using SheWolf.Application.Interfaces.RepositoryInterfaces;
using SheWolf.Domain.Entities;

namespace SheWolf.Application.Queries.Books.GetAll
{
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, OperationResult<List<Book>>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMemoryCache _memoryCache;
        private const string CacheKey = "allBooks";

        public GetAllBooksQueryHandler(IBookRepository bookRepository, IMemoryCache memoryCache)
        {
            _bookRepository = bookRepository;
            _memoryCache = memoryCache;
        }

        public async Task<OperationResult<List<Book>>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            if (!_memoryCache.TryGetValue(CacheKey, out List<Book> allBooks))
            {
                allBooks = await _bookRepository.GetAllBooks();

                if (allBooks == null || !allBooks.Any())
                {
                    return OperationResult<List<Book>>.Failure("No books found in the system.");
                }

                _memoryCache.Set(CacheKey, allBooks, TimeSpan.FromMinutes(10));
            }

            if (allBooks == null || !allBooks.Any())
            {
                return OperationResult<List<Book>>.Failure("No books found in the system.");
            }

            return OperationResult<List<Book>>.Successful(allBooks, "Books retrieved successfully.");
        }
    }
}
