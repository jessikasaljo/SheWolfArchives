using MediatR;
using SheWolf.Domain.Entities;

namespace SheWolf.Application.Queries.Users.GetAll
{
    public class GetAllUsersQuery : IRequest<List<User>>
    {
    }
}
