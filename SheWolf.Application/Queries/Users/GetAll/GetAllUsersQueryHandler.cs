using SheWolf.Domain.Entities;
using SheWolf.Infrastructure.Database;

namespace SheWolf.Application.Queries.Users.GetAll
{
    public class GetAllUsersQueryHandler
    {
        private readonly MockDatabase mockDatabase;

        public GetAllUsersQueryHandler(MockDatabase mockDatabase)
        {
            this.mockDatabase = mockDatabase;
        }
        public Task<List<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            List<User> allUsersFromMockDatabase = mockDatabase.users;

            if (allUsersFromMockDatabase == null || !allUsersFromMockDatabase.Any())
            {
                throw new ArgumentException("Userlist is empty or null");
            }

            return Task.FromResult(allUsersFromMockDatabase);
        }
    }
}
