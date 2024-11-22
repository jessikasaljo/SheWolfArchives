using MediatR;
using SheWolf.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheWolf.Application.Commands.Books.UpdateBook
{
    public class UpdateBookByIdCommand : IRequest<Book>
    {
        public Book UpdatedBook { get; }
        public Guid Id { get; }

        public UpdateBookByIdCommand(Book updatedBook, Guid id)
        {
            UpdatedBook = updatedBook;
            Id = id;
        }
    }
}
