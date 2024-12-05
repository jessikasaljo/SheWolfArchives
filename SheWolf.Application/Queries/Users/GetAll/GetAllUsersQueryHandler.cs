using MediatR;
using SheWolf.Application.Interfaces.RepositoryInterfaces;
using SheWolf.Domain.Entities;

namespace SheWolf.Application.Queries.Users.GetAll
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, OperationResult<List<User>>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<OperationResult<List<User>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            List<User> allUsers = await _userRepository.GetAllUsers();

            if (allUsers == null || !allUsers.Any())
            {
                return OperationResult<List<User>>.Failure("User list is empty or null.");
            }

            return OperationResult<List<User>>.Successful(allUsers, "Users retrieved successfully.");
        }
    }
}
