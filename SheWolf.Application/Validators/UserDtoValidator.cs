using SheWolf.Application.Dtos;
using SheWolf.Application.Exceptions;

namespace SheWolf.Application.Validators
{
    public class UserDtoValidator
    {
        public static void ValidateUserDto(UserDto userDto)
        {
            if (string.IsNullOrWhiteSpace(userDto.Name))
            {
                throw new UserDtoException("Name cannot be null or an empty string");
            }
        }
    }
}