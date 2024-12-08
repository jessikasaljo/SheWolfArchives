using SheWolf.Infrastructure.Database;
using SheWolf.Infrastructure.Repositories;
using SheWolf.Domain.Entities;
using Xunit;
using Microsoft.EntityFrameworkCore;
using SheWolf.Application.Interfaces.RepositoryInterfaces;

namespace SheWolf.Tests.IntegrationTests
{
    public class UserRepositoryTests : IDisposable
    {
        private readonly SheWolf_Database _database;
        private readonly IUserRepository _userRepository;

        public UserRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<SheWolf_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _database = new SheWolf_Database(options);
            _userRepository = new UserRepository(_database);
        }

        public void Dispose()
        {
            _database.Database.EnsureDeleted();
            _database.Dispose();
        }

        [Fact]
        public async Task AddUser_ShouldAddUser_WhenValidDataIsProvided()
        {
            var newUser = new User { Id = Guid.NewGuid(), Username = "testuser", Password = "Password123" };

            var addedUser = await _userRepository.AddUser(newUser);

            var userFromDb = await _userRepository.GetUserById(addedUser.Id);

            Assert.NotNull(userFromDb);
            Assert.Equal(newUser.Username, userFromDb.Username);
        }

        [Fact]
        public async Task GetAllUsers_ShouldReturnAllUsers_WhenCalled()
        {
            var user1 = new User { Id = Guid.NewGuid(), Username = "user1", Password = "Password123" };
            var user2 = new User { Id = Guid.NewGuid(), Username = "user2", Password = "Password123" };

            await _userRepository.AddUser(user1);
            await _userRepository.AddUser(user2);

            var users = await _userRepository.GetAllUsers();

            Assert.Equal(2, users.Count);
        }

        [Fact]
        public async Task GetUserById_ShouldReturnUser_WhenValidIdIsProvided()
        {
            var user = new User { Id = Guid.NewGuid(), Username = "user123", Password = "Password123" };
            await _userRepository.AddUser(user);

            var userFromDb = await _userRepository.GetUserById(user.Id);

            Assert.NotNull(userFromDb);
            Assert.Equal(user.Username, userFromDb.Username);
        }

        [Fact]
        public async Task GetUserById_ShouldReturnNull_WhenInvalidIdIsProvided()
        {
            var userFromDb = await _userRepository.GetUserById(Guid.NewGuid());
            Assert.Null(userFromDb);
        }

        [Fact]
        public async Task Login_ShouldReturnUser_WhenValidCredentialsAreProvided()
        {
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Username = "testuser",
                Password = "Password123"
            };

            var addedUser = await _userRepository.AddUser(newUser);

            Assert.NotEqual("Password123", addedUser.Password);

            var userFromDb = await _userRepository.GetUserById(addedUser.Id);
            Assert.NotNull(userFromDb);

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify("Password123", userFromDb.Password);
            Assert.True(isPasswordValid);

            var loggedInUser = await _userRepository.Login(newUser.Username, "Password123");
            Assert.NotNull(loggedInUser);
            Assert.Equal(newUser.Username, loggedInUser.Username);
        }

        [Fact]
        public async Task Login_ShouldThrowUnauthorizedAccessException_WhenInvalidCredentialsAreProvided()
        {
            var newUser = new User { Id = Guid.NewGuid(), Username = "testuser", Password = "Password123" };
            await _userRepository.AddUser(newUser);

            await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
                _userRepository.Login(newUser.Username, "WrongPassword"));
        }

        [Fact]
        public async Task AddUser_ShouldThrowArgumentNullException_WhenUserIsNull()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _userRepository.AddUser(null));
        }

        [Fact]
        public async Task Login_ShouldThrowArgumentException_WhenUsernameOrPasswordIsNull()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _userRepository.Login(null, "Password123"));
            await Assert.ThrowsAsync<ArgumentException>(() => _userRepository.Login("testuser", null));
        }
    }
}
