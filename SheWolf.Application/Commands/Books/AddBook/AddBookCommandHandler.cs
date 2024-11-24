using MediatR;
using SheWolf.Domain.Entities;
using SheWolf.Infrastructure.Database;

namespace SheWolf.Application.Commands.Books.AddBook
{
    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, Book>
    {
        private readonly MockDatabase mockDatabase;

        public AddBookCommandHandler(MockDatabase mockDatabase)
        {
            this.mockDatabase = mockDatabase;
        }

        public Task<Book> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "AddBookCommand cannot be null.");
            }

            if (request.NewBook == null)
            {
                throw new ArgumentNullException(nameof(request.NewBook), "NewBook cannot be null.");
            }

            Book bookToCreate = new()
            {
                Id = Guid.NewGuid(),
                Title = request.NewBook.Title
            };

            mockDatabase.books.Add(bookToCreate);

            return Task.FromResult(bookToCreate);
        }
    }
}
