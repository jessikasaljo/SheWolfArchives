using MediatR;
using SheWolf.Domain.Entities;
using SheWolf.Application.Interfaces.RepositoryInterfaces;
using SheWolf.Application.Commands.Books.DeleteBook;

namespace SheWolf.Application.Commands.Books.DeleteBook
{
    public class DeleteBookByIdCommandHandler : IRequestHandler<DeleteBookByIdCommand, OperationResult<string>>
    {
        private readonly IBookRepository _bookRepository;

        public DeleteBookByIdCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<OperationResult<string>> Handle(DeleteBookByIdCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return OperationResult<string>.Failure("DeleteBookByIdCommand cannot be null.");
            }

            if (request.Id == Guid.Empty)
            {
                return OperationResult<string>.Failure("Book ID cannot be an empty GUID.");
            }

            var deletedBook = await _bookRepository.DeleteBookById(request.Id);

            if (deletedBook == null)
            {
                return OperationResult<string>.Failure($"No book found with ID {request.Id}.");
            }

            return OperationResult<string>.Successful("Successfully deleted book.");
        }
    }
}
