using MediatR;
using SheWolf.Application.Commands.Authors.DeleteAuthor;
using SheWolf.Domain.Entities;
using SheWolf.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheWolf.Application.Commands.Books.DeleteBook
{
    public class DeleteBookByIdCommandHandler : IRequestHandler<DeleteBookByIdCommand, Book>
    {
        private readonly MockDatabase mockDatabase;

        public DeleteBookByIdCommandHandler(MockDatabase mockDatabase)
        {
            this.mockDatabase = mockDatabase;
        }

        public Task<Book> Handle(DeleteBookByIdCommand request, CancellationToken cancellationToken)
        {
            Book? bookToDelete = mockDatabase.books.FirstOrDefault(author => author.Id == request.Id);

            if (bookToDelete == null)
            {
                return Task.FromResult<Book>(null!);
            }

            mockDatabase.books.Remove(bookToDelete);

            return Task.FromResult(bookToDelete);
        }
    }
}
