using MediatR;
using SheWolf.Domain.Entities;
using SheWolf.Application.DTOs;

namespace SheWolf.Application.Commands.Users.AddUser
{
    public class AddUserCommand : IRequest<OperationResult<UserDto>>
    {
        public User NewUser { get; }

        public AddUserCommand(User newUser)
        {
            NewUser = newUser;
        }
    }
}
