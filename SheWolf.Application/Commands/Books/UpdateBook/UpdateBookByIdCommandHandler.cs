using MediatR;
using SheWolf.Domain.Entities;
using SheWolf.Application.Interfaces.RepositoryInterfaces;

namespace SheWolf.Application.Commands.Books.UpdateBook
{
    public class UpdateBookByIdCommandHandler : IRequestHandler<UpdateBookByIdCommand, Book>
    {
        private readonly IBookRepository _bookRepository;

        public UpdateBookByIdCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Book> Handle(UpdateBookByIdCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "UpdateBookByIdCommand cannot be null.");
            }

            if (request.UpdatedBook == null)
            {
                throw new ArgumentNullException(nameof(request.UpdatedBook), "UpdatedBook cannot be null.");
            }

            if (request.Id == Guid.Empty)
            {
                throw new ArgumentException("Book ID cannot be an empty GUID.");
            }

            var updatedBook = await _bookRepository.UpdateBook(request.Id, request.UpdatedBook);

            if (updatedBook == null)
            {
                throw new InvalidOperationException($"Failed to update book. No book found with Id: {request.Id}");
            }

            return updatedBook;
        }
    }
}
