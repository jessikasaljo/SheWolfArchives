using MediatR;
using SheWolf.Domain.Entities;
using SheWolf.Application.Interfaces.RepositoryInterfaces;

namespace SheWolf.Application.Commands.Books.DeleteBook
{
    public class DeleteBookByIdCommandHandler : IRequestHandler<DeleteBookByIdCommand, string>
    {
        private readonly IBookRepository _bookRepository;

        public DeleteBookByIdCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<string> Handle(DeleteBookByIdCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "DeleteBookByIdCommand cannot be null.");
            }

            if (request.Id == Guid.Empty)
            {
                throw new ArgumentException("Book ID cannot be an empty GUID.");
            }

            var deletedBook = await _bookRepository.DeleteBookById(request.Id);

            return deletedBook;
        }
    }
}
