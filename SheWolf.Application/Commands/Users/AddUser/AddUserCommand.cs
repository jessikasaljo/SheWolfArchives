using MediatR;
using SheWolf.Domain.Entities;

namespace SheWolf.Application.Commands.Users.AddUser
{
    public class AddUserCommand : IRequest<User>
    {
        public User NewUser { get; }

        public AddUserCommand(User newUser)
        {
            NewUser = newUser;
        }
    }
}
