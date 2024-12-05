using MediatR;
using SheWolf.Domain.Entities;

namespace SheWolf.Application.Queries.Authors.GetById
{
    public class GetAuthorByIdQuery : IRequest<OperationResult<Author>>
    {
        public Guid Id { get; }

        public GetAuthorByIdQuery(Guid authorId)
        {
            Id = authorId;
        }
    }
}
