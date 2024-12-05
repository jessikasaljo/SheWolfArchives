using MediatR;
using SheWolf.Domain.Entities;

namespace SheWolf.Application.Queries.Users.GetById
{
    public class GetUserByIdQuery : IRequest<OperationResult<User>>
    {
        public Guid Id { get; }

        public GetUserByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}