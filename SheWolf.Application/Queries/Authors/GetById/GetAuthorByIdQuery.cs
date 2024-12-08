using MediatR;
using SheWolf.Domain.Entities;
using SheWolf.Application.DTOs;

namespace SheWolf.Application.Queries.Authors.GetById
{
    public class GetAuthorByIdQuery : IRequest<OperationResult<AuthorDto>>
    {
        public Guid Id { get; }

        public GetAuthorByIdQuery(Guid authorId)
        {
            Id = authorId;
        }
    }
}
