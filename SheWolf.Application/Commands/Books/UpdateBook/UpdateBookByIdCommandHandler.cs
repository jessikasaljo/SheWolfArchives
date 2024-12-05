using MediatR;
using SheWolf.Domain.Entities;
using SheWolf.Application.Interfaces.RepositoryInterfaces;

namespace SheWolf.Application.Commands.Books.UpdateBook
{
    public class UpdateBookByIdCommandHandler : IRequestHandler<UpdateBookByIdCommand, OperationResult<Book>>
    {
        private readonly IBookRepository _bookRepository;

        public UpdateBookByIdCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<OperationResult<Book>> Handle(UpdateBookByIdCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return OperationResult<Book>.Failure("UpdateBookByIdCommand cannot be null.");
            }

            if (request.UpdatedBook == null)
            {
                return OperationResult<Book>.Failure("UpdatedBook cannot be null.");
            }

            if (request.Id == Guid.Empty)
            {
                return OperationResult<Book>.Failure("Book ID cannot be an empty GUID.");
            }

            var updatedBook = await _bookRepository.UpdateBook(request.Id, request.UpdatedBook);

            if (updatedBook == null)
            {
                return OperationResult<Book>.Failure($"Failed to update book. No book found with Id: {request.Id}");
            }

            return OperationResult<Book>.Successful(updatedBook);
        }
    }
}
