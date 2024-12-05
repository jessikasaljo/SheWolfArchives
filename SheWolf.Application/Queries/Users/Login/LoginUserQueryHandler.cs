using MediatR;
using SheWolf.Application.Interfaces.RepositoryInterfaces;
using SheWolf.Application.Queries.Users.Login.Helpers;
using SheWolf.Domain.Entities;

namespace SheWolf.Application.Queries.Users.Login
{
    public class LogInUserQueryHandler : IRequestHandler<LogInUserQuery, OperationResult<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly TokenHelper _tokenHelper;

        public LogInUserQueryHandler(IUserRepository userRepository, TokenHelper tokenHelper)
        {
            _userRepository = userRepository;
            _tokenHelper = tokenHelper;
        }

        public async Task<OperationResult<string>> Handle(LogInUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Login(request.Username, request.Password);

            if (user == null)
            {
                return OperationResult<string>.Failure("Invalid username or password");
            }

            string token = _tokenHelper.GenerateJwtToken(user);

            return OperationResult<string>.Successful(token, "Login successful");
        }
    }
}
