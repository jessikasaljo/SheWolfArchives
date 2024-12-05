using MediatR;
using SheWolf.Application.Interfaces.RepositoryInterfaces;
using SheWolf.Domain.Entities;

namespace SheWolf.Application.Commands.Authors.AddAuthor
{
    public class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand, OperationResult<Author>>
    {
        private readonly IAuthorRepository _authorRepository;

        public AddAuthorCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<OperationResult<Author>> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return OperationResult<Author>.Failure("AddAuthorCommand cannot be null.");
            }

            if (request.NewAuthor == null)
            {
                return OperationResult<Author>.Failure("NewAuthor cannot be null.");
            }

            var addedAuthor = await _authorRepository.AddAuthor(request.NewAuthor);

            if (addedAuthor == null)
            {
                return OperationResult<Author>.Failure("Failed to add the author.");
            }

            return OperationResult<Author>.Successful(addedAuthor, "Author added successfully.");
        }
    }
}
