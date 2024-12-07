using MediatR;
using Microsoft.Extensions.Caching.Memory;
using SheWolf.Application.Interfaces.RepositoryInterfaces;
using SheWolf.Domain.Entities;

namespace SheWolf.Application.Queries.Authors.GetAll
{
    public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, OperationResult<List<Author>>>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMemoryCache _memoryCache;
        private const string cacheKey = "allAuthors";

        public GetAllAuthorsQueryHandler(IAuthorRepository authorRepository, IMemoryCache memoryCache)
        {
            _authorRepository = authorRepository;
            _memoryCache = memoryCache;
        }

        public async Task<OperationResult<List<Author>>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            if (_memoryCache.TryGetValue(cacheKey, out List<Author> cachedAuthors))
            {
                return OperationResult<List<Author>>.Successful(cachedAuthors, "Authors retrieved successfully from cache.");
            }

            List<Author> allAuthors = await _authorRepository.GetAllAuthors();

            if (allAuthors == null || !allAuthors.Any())
            {
                return OperationResult<List<Author>>.Failure("Author list is empty or null.");
            }

            _memoryCache.Set(cacheKey, allAuthors, TimeSpan.FromMinutes(10));

            return OperationResult<List<Author>>.Successful(allAuthors, "Authors retrieved successfully.");
        }
    }
}
