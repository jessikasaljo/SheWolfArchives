using MediatR;
using SheWolf.Application.Interfaces.RepositoryInterfaces;
using SheWolf.Domain.Entities;

namespace SheWolf.Application.Commands.Authors.UpdateAuthor
{
    public class UpdateAuthorByIdCommandHandler : IRequestHandler<UpdateAuthorByIdCommand, OperationResult<Author>>
    {
        private readonly IAuthorRepository _authorRepository;

        public UpdateAuthorByIdCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<OperationResult<Author>> Handle(UpdateAuthorByIdCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return OperationResult<Author>.Failure("UpdateAuthorByIdCommand cannot be null.");
            }

            if (request.UpdatedAuthor == null)
            {
                return OperationResult<Author>.Failure("UpdatedAuthor cannot be null.");
            }

            if (request.Id == Guid.Empty)
            {
                return OperationResult<Author>.Failure("Author ID cannot be an empty GUID.");
            }

            var updatedAuthor = await _authorRepository.UpdateAuthor(request.Id, request.UpdatedAuthor);

            if (updatedAuthor == null)
            {
                return OperationResult<Author>.Failure($"Failed to update author. No author found with Id: {request.Id}");
            }

            return OperationResult<Author>.Successful(updatedAuthor, "Author updated successfully.");
        }
    }
}