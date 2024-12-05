using MediatR;
using SheWolf.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheWolf.Application.Commands.Books.AddBook
{
    public class AddBookCommand : IRequest<OperationResult<Book>>
    {
        public Book NewBook { get; }

        public AddBookCommand(Book newBook)
        {
            NewBook = newBook;
        }
    }
}
