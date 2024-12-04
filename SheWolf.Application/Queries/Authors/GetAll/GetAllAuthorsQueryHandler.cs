using MediatR;
using SheWolf.Application.Interfaces.RepositoryInterfaces;
using SheWolf.Domain.Entities;

namespace SheWolf.Application.Queries.Authors.GetAll
{
    public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, List<Author>>
    {
        private readonly IAuthorRepository _authorRepository;

        public GetAllAuthorsQueryHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<List<Author>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            List<Author> allAuthors = await _authorRepository.GetAllAuthors();

            if (allAuthors == null || !allAuthors.Any())
            {
                throw new ArgumentException("Authorlist is empty or null");
            }

            return allAuthors;
        }
    }
}
