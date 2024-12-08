using MediatR;
using SheWolf.Application.Interfaces.RepositoryInterfaces;
using SheWolf.Domain.Entities;
using SheWolf.Application.DTOs;
using SheWolf.Application.Mappers;

namespace SheWolf.Application.Commands.Authors.AddAuthor
{
    public class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand, OperationResult<AuthorDto>>
    {
        private readonly IAuthorRepository _authorRepository;

        public AddAuthorCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<OperationResult<AuthorDto>> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return OperationResult<AuthorDto>.Failure("AddAuthorCommand cannot be null.");
            }

            if (request.NewAuthor == null)
            {
                return OperationResult<AuthorDto>.Failure("NewAuthor cannot be null.");
            }

            var addedAuthor = await _authorRepository.AddAuthor(request.NewAuthor);

            if (addedAuthor == null)
            {
                return OperationResult<AuthorDto>.Failure("Failed to add the author.");
            }

            var addedAuthorDto = EntityMapper.MapToDto(addedAuthor);

            return OperationResult<AuthorDto>.Successful(addedAuthorDto, "Author added successfully.");
        }
    }
}
