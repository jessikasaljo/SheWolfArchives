using MediatR;
using SheWolf.Application.Commands.Authors.UpdateAuthor;
using SheWolf.Domain.Entities;
using SheWolf.Infrastructure.Database;

namespace SheWolf.Application.Commands.Books.UpdateBook
{
    public class UpdateBookByIdCommandHandler : IRequestHandler<UpdateBookByIdCommand, Book>
    {
        private readonly MockDatabase mockDatabase;

        public UpdateBookByIdCommandHandler(MockDatabase mockDatabase)
        {
            this.mockDatabase = mockDatabase;
        }

        public Task<Book> Handle(UpdateBookByIdCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "UpdateBookByIdCommand cannot be null.");
            }

            if (request.UpdatedBook == null)
            {
                throw new ArgumentNullException(nameof(request.UpdatedBook), "UpdatedBook cannot be null.");
            }

            Book bookToUpdate = mockDatabase.books.FirstOrDefault(book => book.Id == request.Id)!;

            if (bookToUpdate == null)
            {
                return Task.FromResult<Book>(null!);
            }

            bookToUpdate.Title = request.UpdatedBook.Title;

            return Task.FromResult(bookToUpdate);
        }
    }
}
