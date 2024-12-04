using MediatR;
using SheWolf.Domain.Entities;
using SheWolf.Application.Interfaces.RepositoryInterfaces;

namespace SheWolf.Application.Queries.Authors.GetById
{
    public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, Author>
    {
        private readonly IAuthorRepository _authorRepository;

        public GetAuthorByIdQueryHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Author> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "GetAuthorByIdQuery cannot be null.");
            }

            Author wantedAuthor = await _authorRepository.GetAuthorById(request.Id);

            if (wantedAuthor == null)
            {
                throw new InvalidOperationException($"Author with ID {request.Id} not found.");
            }

            return wantedAuthor;
        }
    }
}
