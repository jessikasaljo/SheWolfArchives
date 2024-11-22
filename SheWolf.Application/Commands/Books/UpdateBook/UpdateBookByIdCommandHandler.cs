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
            Book bookToUpdate = mockDatabase.books.FirstOrDefault(book => book.Id == request.Id)!;

            bookToUpdate.Title = request.UpdatedBook.Title;

            return Task.FromResult(bookToUpdate);
        }
    }
}
