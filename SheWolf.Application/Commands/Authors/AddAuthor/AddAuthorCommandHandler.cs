using MediatR;
using SheWolf.Application.Interfaces.RepositoryInterfaces;
using SheWolf.Domain.Entities;

namespace SheWolf.Application.Commands.Authors.AddAuthor
{
    public class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand, Author>
    {
        private readonly IAuthorRepository _authorRepository;

        public AddAuthorCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Author> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "AddAuthorCommand cannot be null.");
            }

            if (request.NewAuthor == null)
            {
                throw new ArgumentNullException(nameof(request.NewAuthor), "NewAuthor cannot be null.");
            }

            return await _authorRepository.AddAuthor(request.NewAuthor);
        }
    }
}
