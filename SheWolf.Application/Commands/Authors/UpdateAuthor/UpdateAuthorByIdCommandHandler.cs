using MediatR;
using SheWolf.Application.Interfaces.RepositoryInterfaces;
using SheWolf.Domain.Entities;
using SheWolf.Application.DTOs;
using SheWolf.Application.Mappers;

namespace SheWolf.Application.Commands.Authors.UpdateAuthor
{
    public class UpdateAuthorByIdCommandHandler : IRequestHandler<UpdateAuthorByIdCommand, OperationResult<AuthorDto>>
    {
        private readonly IAuthorRepository _authorRepository;

        public UpdateAuthorByIdCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<OperationResult<AuthorDto>> Handle(UpdateAuthorByIdCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return OperationResult<AuthorDto>.Failure("UpdateAuthorByIdCommand cannot be null.");
            }

            if (request.UpdatedAuthor == null)
            {
                return OperationResult<AuthorDto>.Failure("UpdatedAuthor cannot be null.");
            }

            if (request.Id == Guid.Empty)
            {
                return OperationResult<AuthorDto>.Failure("Author ID cannot be an empty GUID.");
            }

            var authorToUpdate = EntityMapper.MapToDto(request.UpdatedAuthor);
            var updatedAuthorDto = await _authorRepository.UpdateAuthor(request.Id, authorToUpdate);
            var updatedAuthor = EntityMapper.MapToDto(updatedAuthorDto);

            if (updatedAuthorDto == null)
            {
                return OperationResult<AuthorDto>.Failure($"Failed to update author. No author found with Id: {request.Id}");
            }

            return OperationResult<AuthorDto>.Successful(updatedAuthor, "Author updated successfully.");
        }
    }
}