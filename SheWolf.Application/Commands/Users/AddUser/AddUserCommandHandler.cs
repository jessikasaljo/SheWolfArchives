using MediatR;
using SheWolf.Application.Interfaces.RepositoryInterfaces;
using SheWolf.Domain.Entities;
using SheWolf.Application.DTOs;
using SheWolf.Application.Mappers;

namespace SheWolf.Application.Commands.Users.AddUser
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, OperationResult<UserDto>>
    {
        private readonly IUserRepository _userRepository;

        public AddUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<OperationResult<UserDto>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return OperationResult<UserDto>.Failure("AddUserCommand cannot be null.");
            }

            if (request.NewUser == null)
            {
                return OperationResult<UserDto>.Failure("NewUser cannot be null.");
            }

            var addedUser = await _userRepository.AddUser(request.NewUser);

            if (addedUser == null)
            {
                return OperationResult<UserDto>.Failure("Failed to add the user.");
            }

            var addedUserDto = EntityMapper.MapToDto(addedUser);

            return OperationResult<UserDto>.Successful(addedUserDto, "User added successfully.");
        }
    }
}
