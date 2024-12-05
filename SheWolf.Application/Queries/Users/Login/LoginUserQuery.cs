using MediatR;
using SheWolf.Domain.Entities;

namespace SheWolf.Application.Queries.Users.Login
{
    public class LogInUserQuery : IRequest<OperationResult<string>>
    {
        public string Username { get; }
        public string Password { get; }

        public LogInUserQuery(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}