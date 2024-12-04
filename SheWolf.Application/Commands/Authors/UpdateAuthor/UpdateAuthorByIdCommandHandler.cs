using MediatR;
using SheWolf.Domain.Entities;
using SheWolf.Application.Interfaces.RepositoryInterfaces;

namespace SheWolf.Application.Commands.Authors.UpdateAuthor
{
    public class UpdateAuthorByIdCommandHandler : IRequestHandler<UpdateAuthorByIdCommand, Author>
    {
        private readonly IAuthorRepository _authorRepository;

        public UpdateAuthorByIdCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Author> Handle(UpdateAuthorByIdCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "UpdateAuthorByIdCommand cannot be null.");
            }

            if (request.UpdatedAuthor == null)
            {
                throw new ArgumentNullException(nameof(request.UpdatedAuthor), "UpdatedAuthor cannot be null.");
            }

            if (request.Id == Guid.Empty)
            {
                throw new ArgumentException("Author ID cannot be an empty GUID.");
            }

            var updatedAuthor = await _authorRepository.UpdateAuthor(request.Id, request.UpdatedAuthor);

            if (updatedAuthor == null)
            {
                throw new InvalidOperationException($"Failed to update author. No author found with Id: {request.Id}");
            }

            return updatedAuthor;
        }
    }
}