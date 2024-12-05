using SheWolf.Application.Interfaces.RepositoryInterfaces;
using SheWolf.Domain.Entities;

namespace SheWolf.Application.Queries.Users.GetAll
{
    public class GetAllUsersQueryHandler
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<List<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            List<User> allUsers = await _userRepository.GetAllUsers();

            if (allUsers == null || !allUsers.Any())
            {
                throw new ArgumentException("Userlist is empty or null");
            }

            return allUsers;
        }
    }
}
