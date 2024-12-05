using MediatR;
using SheWolf.Application.Interfaces.RepositoryInterfaces;
using SheWolf.Domain.Entities;

namespace SheWolf.Application.Commands.Users.AddUser
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, OperationResult<User>>
    {
        private readonly IUserRepository _userRepository;

        public AddUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<OperationResult<User>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return OperationResult<User>.Failure("AddUserCommand cannot be null.");
            }

            if (request.NewUser == null)
            {
                return OperationResult<User>.Failure("NewUser cannot be null.");
            }

            var addedUser = await _userRepository.AddUser(request.NewUser);

            if (addedUser == null)
            {
                return OperationResult<User>.Failure("Failed to add the user.");
            }

            return OperationResult<User>.Successful(addedUser, "User added successfully.");
        }
    }
}
