using MediatR;
using SheWolf.Application.Interfaces.RepositoryInterfaces;
using SheWolf.Domain.Entities;
using SheWolf.Application.DTOs;
using SheWolf.Application.Mappers;

namespace SheWolf.Application.Commands.Books.AddBook
{
    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, OperationResult<BookDto>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;

        public AddBookCommandHandler(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }

        public async Task<OperationResult<BookDto>> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return OperationResult<BookDto>.Failure("AddBookCommand cannot be null.");
            }

            if (request.NewBook == null)
            {
                return OperationResult<BookDto>.Failure("NewBook cannot be null.");
            }

            var author = await _authorRepository.GetAuthorById(request.NewBook.AuthorId);
            if (author == null)
            {
                return OperationResult<BookDto>.Failure("Author not found.");
            }

            var addedBook = await _bookRepository.AddBook(request.NewBook);

            if (addedBook == null)
            {
                return OperationResult<BookDto>.Failure("Failed to add the book.");
            }

            var addedBookDto = EntityMapper.MapToDto(addedBook);

            return OperationResult<BookDto>.Successful(addedBookDto, "Book added successfully.");
        }
    }
}