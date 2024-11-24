using MediatR;
using SheWolf.Application.Queries.Users.Login.Helpers;
using SheWolf.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheWolf.Application.Queries.Users.Login
{
    public class LogInUserQueryHandler : IRequestHandler<LogInUserQuery, string>
    {
        private readonly MockDatabase mockDatabase;
        private readonly TokenHelper tokenHelper;

        public LogInUserQueryHandler(MockDatabase mockDatabase, TokenHelper tokenHelper)
        {
            this.mockDatabase = mockDatabase;
            this.tokenHelper = tokenHelper;
        }
        public Task<string> Handle(LogInUserQuery request, CancellationToken cancellationToken)
        {
            var user = mockDatabase.users.FirstOrDefault(user => user.Username == request.LogInUser.Username && user.Password == request.LogInUser.Password);
            if (user == null)

            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            string token = tokenHelper.GenerateJwtToken(user);
            return Task.FromResult(token);
        }
    }
}
