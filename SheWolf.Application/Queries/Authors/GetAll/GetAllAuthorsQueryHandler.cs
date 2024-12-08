using MediatR;
using Microsoft.Extensions.Caching.Memory;
using SheWolf.Application.Interfaces.RepositoryInterfaces;
using SheWolf.Domain.Entities;
using SheWolf.Application.DTOs;
using SheWolf.Application.Mappers;

namespace SheWolf.Application.Queries.Authors.GetAll
{
    public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, OperationResult<List<AuthorDto>>>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMemoryCache _memoryCache;
        private const string cacheKey = "allAuthors";

        public GetAllAuthorsQueryHandler(IAuthorRepository authorRepository, IMemoryCache memoryCache)
        {
            _authorRepository = authorRepository;
            _memoryCache = memoryCache;
        }

        public async Task<OperationResult<List<AuthorDto>>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            if (_memoryCache.TryGetValue(cacheKey, out List<AuthorDto> cachedAuthors))
            {
                return OperationResult<List<AuthorDto>>.Successful(cachedAuthors, "Authors retrieved successfully from cache.");
            }

            List<Author> allAuthors = await _authorRepository.GetAllAuthors();

            if (allAuthors == null || !allAuthors.Any())
            {
                return OperationResult<List<AuthorDto>>.Failure("Author list is empty or null.");
            }

            var authorDtos = allAuthors.Select(EntityMapper.MapToDto).ToList();

            _memoryCache.Set(cacheKey, authorDtos, TimeSpan.FromMinutes(10));

            return OperationResult<List<AuthorDto>>.Successful(authorDtos, "Authors retrieved successfully.");
        }
    }
}
