using MediatR;
using SheWolf.Domain.Entities;

namespace SheWolf.Application.Queries.Users.Login
{
    public class LogInUserQuery : IRequest<String>
    {
        public User LogInUser { get; }

        public LogInUserQuery(User logInUser)
        {
            LogInUser = logInUser;
        }
    }
}
