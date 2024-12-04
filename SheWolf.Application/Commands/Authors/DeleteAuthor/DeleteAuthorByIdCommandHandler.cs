using MediatR;
using SheWolf.Domain.Entities;
using SheWolf.Application.Interfaces.RepositoryInterfaces;

namespace SheWolf.Application.Commands.Authors.DeleteAuthor
{
    public class DeleteAuthorByIdCommandHandler : IRequestHandler<DeleteAuthorByIdCommand, string>
    {
        private readonly IAuthorRepository _authorRepository;

        public DeleteAuthorByIdCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<string> Handle(DeleteAuthorByIdCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "DeleteAuthorByIdCommand cannot be null.");
            }

            if (request.Id == Guid.Empty)
            {
                throw new ArgumentException("Author ID cannot be an empty GUID.");
            }

            var deletedAuthor = await _authorRepository.DeleteAuthorById(request.Id);

            return deletedAuthor;
        }
    }
}
