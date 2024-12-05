using MediatR;
using SheWolf.Domain.Entities;
using SheWolf.Application.Interfaces.RepositoryInterfaces;

namespace SheWolf.Application.Queries.Authors.GetById
{
    public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, OperationResult<Author>>
    {
        private readonly IAuthorRepository _authorRepository;

        public GetAuthorByIdQueryHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<OperationResult<Author>> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return OperationResult<Author>.Failure("GetAuthorByIdQuery cannot be null.");
            }

            Author wantedAuthor = await _authorRepository.GetAuthorById(request.Id);

            if (wantedAuthor == null)
            {
                return OperationResult<Author>.Failure($"Author with ID {request.Id} not found.");
            }

            return OperationResult<Author>.Successful(wantedAuthor, "Author retrieved successfully.");
        }
    }
}