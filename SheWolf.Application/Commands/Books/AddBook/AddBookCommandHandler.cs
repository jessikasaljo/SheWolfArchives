using MediatR;
using SheWolf.Application.Commands.Authors.AddAuthor;
using SheWolf.Domain.Entities;
using SheWolf.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
