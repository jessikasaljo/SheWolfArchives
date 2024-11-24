using MediatR;
using SheWolf.Domain.Entities;
using SheWolf.Infrastructure.Database;

namespace SheWolf.Application.Commands.Users.AddUser
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, User>
    {
        private readonly MockDatabase mockDatabase;

        public AddUserCommandHandler(MockDatabase mockDatabase)
        {
            this.mockDatabase = mockDatabase;
        }
        public Task<User> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.NewUser == null ||
              string.IsNullOrWhiteSpace(request.NewUser.Username) ||
              string.IsNullOrWhiteSpace(request.NewUser.Password))
            {
                throw new ArgumentException("Author name and description cannot be empty or null");
            }

            User userToCreate = new()
            {
                Id = Guid.NewGuid(),
                Username = request.NewUser.Username,
                Password = request.NewUser.Password,
            };

            mockDatabase.users.Add(userToCreate);

            return Task.FromResult(userToCreate);
        }
    }
}
