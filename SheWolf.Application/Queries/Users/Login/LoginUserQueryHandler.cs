using MediatR;
using SheWolf.Application.Queries.Users.Login.Helpers;
using SheWolf.Application.Interfaces.RepositoryInterfaces;

namespace SheWolf.Application.Queries.Users.Login
{
    public class LogInUserQueryHandler : IRequestHandler<LogInUserQuery, string>
    {
        private readonly IUserRepository _userRepository; // Inject the repository
        private readonly TokenHelper _tokenHelper;

        public LogInUserQueryHandler(IUserRepository userRepository, TokenHelper tokenHelper)
        {
            _userRepository = userRepository;
            _tokenHelper = tokenHelper;
        }

        public async Task<string> Handle(LogInUserQuery request, CancellationToken cancellationToken)
        {
            // Use the Login method in the UserRepository to authenticate the user
            var user = await _userRepository.Login(request.LogInUser.Username, request.LogInUser.Password);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            string token = _tokenHelper.GenerateJwtToken(user); // Generate token using TokenHelper
            return token;
        }
    }
}
