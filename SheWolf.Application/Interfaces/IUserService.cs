using SheWolf.Application.Dtos;

namespace SheWolf.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetUserById(int id);
    }
}
