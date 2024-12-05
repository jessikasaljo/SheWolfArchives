using MediatR;
using SheWolf.Application.Interfaces.RepositoryInterfaces;
using SheWolf.Domain.Entities;

namespace SheWolf.Application.Commands.Books.AddBook
{
    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, OperationResult<Book>>
    {
        private readonly IBookRepository _bookRepository;

        public AddBookCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<OperationResult<Book>> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return OperationResult<Book>.Failure("AddBookCommand cannot be null.");
            }

            if (request.NewBook == null)
            {
                return OperationResult<Book>.Failure("NewBook cannot be null.");
            }

            var addedBook = await _bookRepository.AddBook(request.NewBook);

            if (addedBook == null)
            {
                return OperationResult<Book>.Failure("Failed to add the book.");
            }

            return OperationResult<Book>.Successful(addedBook, "Book added successfully.");
        }
    }
}
