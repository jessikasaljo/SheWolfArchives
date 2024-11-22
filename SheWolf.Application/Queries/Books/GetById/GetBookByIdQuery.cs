using MediatR;
using SheWolf.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheWolf.Application.Queries.Books.GetById
{
    public class GetBookByIdQuery : IRequest<Book>
    {
        public Guid Id { get; }

        public GetBookByIdQuery(Guid bookId)
        {
            Id = bookId;
        }
    }
}
