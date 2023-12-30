using LeHotelMax.Application.Users.Dtos;

namespace LeHotelMax.Application.Users.Interfaces
{
    public interface IIdentityRepository
    {
        Task<string> AuthenticateAsync(UserDto userDto);
        Task<bool> PasswordSignInAsync(UserDto userDto, string password);
        Task<UserDto> FindByEmailAsync(string username);
       
    }
}
