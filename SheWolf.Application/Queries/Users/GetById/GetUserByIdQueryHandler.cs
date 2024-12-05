using MediatR;
using SheWolf.Application.Interfaces.RepositoryInterfaces;
using SheWolf.Domain.Entities;

namespace SheWolf.Application.Queries.Users.GetById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, OperationResult<User>>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<OperationResult<User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return OperationResult<User>.Failure("GetUserByIdQuery cannot be null.");
            }

            if (request.Id == Guid.Empty)
            {
                return OperationResult<User>.Failure("User ID cannot be empty.");
            }

            var user = await _userRepository.GetUserById(request.Id);

            if (user == null)
            {
                return OperationResult<User>.Failure($"No user found with ID {request.Id}");
            }

            return OperationResult<User>.Successful(user, "User retrieved successfully.");
        }
    }
}
