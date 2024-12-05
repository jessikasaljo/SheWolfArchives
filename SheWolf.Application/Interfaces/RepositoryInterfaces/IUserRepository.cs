using SheWolf.Domain.Entities;

namespace SheWolf.Application.Interfaces.RepositoryInterfaces
{
    public interface IUserRepository
    {
        Task<User> AddUser(User user);
        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(Guid id);
        Task<User> Login(string username, string password);

    }
}
