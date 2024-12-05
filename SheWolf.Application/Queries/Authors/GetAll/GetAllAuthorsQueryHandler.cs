using MediatR;
using SheWolf.Application.Interfaces.RepositoryInterfaces;
using SheWolf.Domain.Entities;

namespace SheWolf.Application.Queries.Authors.GetAll
{
    public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, OperationResult<List<Author>>>
    {
        private readonly IAuthorRepository _authorRepository;

        public GetAllAuthorsQueryHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<OperationResult<List<Author>>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            List<Author> allAuthors = await _authorRepository.GetAllAuthors();

            if (allAuthors == null || !allAuthors.Any())
            {
                return OperationResult<List<Author>>.Failure("Author list is empty or null.");
            }

            return OperationResult<List<Author>>.Successful(allAuthors, "Authors retrieved successfully.");
        }
    }
}
