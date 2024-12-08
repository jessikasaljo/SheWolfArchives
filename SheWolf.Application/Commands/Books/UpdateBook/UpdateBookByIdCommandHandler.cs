using MediatR;
using SheWolf.Domain.Entities;
using SheWolf.Application.Interfaces.RepositoryInterfaces;
using SheWolf.Application.DTOs;
using SheWolf.Application.Mappers;

namespace SheWolf.Application.Commands.Books.UpdateBook
{
    public class UpdateBookByIdCommandHandler : IRequestHandler<UpdateBookByIdCommand, OperationResult<BookDto>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;

        public UpdateBookByIdCommandHandler(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }

        public async Task<OperationResult<BookDto>> Handle(UpdateBookByIdCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return OperationResult<BookDto>.Failure("UpdateBookByIdCommand cannot be null.");
            }

            if (request.UpdatedBook == null)
            {
                return OperationResult<BookDto>.Failure("UpdatedBook cannot be null.");
            }

            if (request.Id == Guid.Empty)
            {
                return OperationResult<BookDto>.Failure("Book ID cannot be an empty GUID.");
            }

            var author = await _authorRepository.GetAuthorById(request.UpdatedBook.AuthorId);
            if (author == null)
            {
                return OperationResult<BookDto>.Failure("Author not found.");
            }

            var updatedBook = await _bookRepository.UpdateBook(request.Id, request.UpdatedBook);

            if (updatedBook == null)
            {
                return OperationResult<BookDto>.Failure($"Failed to update book. No book found with Id: {request.Id}");
            }

            var updatedBookDto = EntityMapper.MapToDto(updatedBook);

            return OperationResult<BookDto>.Successful(updatedBookDto, "Book updated successfully.");
        }
    }
}