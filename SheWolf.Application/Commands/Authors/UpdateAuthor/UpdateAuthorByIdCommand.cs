using MediatR;
using SheWolf.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheWolf.Application.Commands.Authors.UpdateAuthor
{
    public class UpdateAuthorByIdCommand : IRequest<Author>
    {
        public Author UpdatedAuthor { get; }
        public Guid Id { get; }

        public UpdateAuthorByIdCommand(Author updatedAuthor, Guid id)
        {
            UpdatedAuthor = updatedAuthor;
            Id = id;
        }
    }
}
