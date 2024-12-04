using Microsoft.EntityFrameworkCore;
using SheWolf.Application.Interfaces.RepositoryInterfaces;
using SheWolf.Domain.Entities;
using SheWolf.Infrastructure.Database;

namespace SheWolf.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SheWolf_Database _database;

        public UserRepository(SheWolf_Database database)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database), "Database cannot be null.");
        }

        public async Task<User> AddUser(User newUser)
        {
            _database.Users.Add(newUser);
            await _database.SaveChangesAsync();
            return newUser;
        }

        public async Task<List<User>> GetAllUsers()
        {
            if (_database?.Users == null)
            {
                throw new InvalidOperationException("Users table is null or database is not properly initialized.");
            }

            return await _database.Users.ToListAsync();
        }

        public async Task<User> Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Username and password cannot be null or empty.");
            }

            if (_database?.Users == null)
            {
                throw new InvalidOperationException("Users table is null or database is not properly initialized.");
            }

            return await _database.Users.FirstOrDefaultAsync(user => user.Username == username && user.Password == password);
        }
    }
}
