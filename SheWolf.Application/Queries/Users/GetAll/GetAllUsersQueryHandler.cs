using MediatR;
using Microsoft.Extensions.Caching.Memory;
using SheWolf.Application.Interfaces.RepositoryInterfaces;
using SheWolf.Domain.Entities;

namespace SheWolf.Application.Queries.Users.GetAll
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, OperationResult<List<User>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMemoryCache _memoryCache;
        private const string CacheKey = "allUsers";

        public GetAllUsersQueryHandler(IUserRepository userRepository, IMemoryCache memoryCache)
        {
            _userRepository = userRepository;
            _memoryCache = memoryCache;
        }

        public async Task<OperationResult<List<User>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            if (!_memoryCache.TryGetValue(CacheKey, out List<User> allUsers))
            {
                allUsers = await _userRepository.GetAllUsers();

                if (allUsers == null || !allUsers.Any())
                {
                    return OperationResult<List<User>>.Failure("User list is empty or null.");
                }

                _memoryCache.Set(CacheKey, allUsers, TimeSpan.FromMinutes(10));
            }

            return OperationResult<List<User>>.Successful(allUsers, "Users retrieved successfully.");
        }
    }
}
