using MediatR;
using SheWolf.Domain.Entities;
using SheWolf.Application.Interfaces.RepositoryInterfaces;
using SheWolf.Application.DTOs;
using SheWolf.Application.Mappers;

namespace SheWolf.Application.Queries.Authors.GetById
{
    public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, OperationResult<AuthorDto>>
    {
        private readonly IAuthorRepository _authorRepository;

        public GetAuthorByIdQueryHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<OperationResult<AuthorDto>> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return OperationResult<AuthorDto>.Failure("GetAuthorByIdQuery cannot be null.");
            }

            Author wantedAuthor = await _authorRepository.GetAuthorById(request.Id);

            if (wantedAuthor == null)
            {
                return OperationResult<AuthorDto>.Failure($"Author with ID {request.Id} not found.");
            }

            var authorDto = EntityMapper.MapToDto(wantedAuthor);

            return OperationResult<AuthorDto>.Successful(authorDto, "Author retrieved successfully.");
        }
    }
}
