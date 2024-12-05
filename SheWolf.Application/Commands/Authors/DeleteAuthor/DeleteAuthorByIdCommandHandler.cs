using MediatR;
using SheWolf.Application.Interfaces.RepositoryInterfaces;
using SheWolf.Domain.Entities;

namespace SheWolf.Application.Commands.Authors.DeleteAuthor
{
    public class DeleteAuthorByIdCommandHandler : IRequestHandler<DeleteAuthorByIdCommand, OperationResult<string>>
    {
        private readonly IAuthorRepository _authorRepository;

        public DeleteAuthorByIdCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<OperationResult<string>> Handle(DeleteAuthorByIdCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return OperationResult<string>.Failure("DeleteAuthorByIdCommand cannot be null.");
            }

            if (request.Id == Guid.Empty)
            {
                return OperationResult<string>.Failure("Author ID cannot be an empty GUID.");
            }

            var deletedAuthor = await _authorRepository.DeleteAuthorById(request.Id);

            if (deletedAuthor == null)
            {
                return OperationResult<string>.Failure($"No author found with ID {request.Id}.");
            }

            return OperationResult<string>.Successful(deletedAuthor, "Author deleted successfully.");
        }
    }
}
