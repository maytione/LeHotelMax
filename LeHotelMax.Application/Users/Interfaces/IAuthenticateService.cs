using LeHotelMax.Application.Users.Dtos;

namespace LeHotelMax.Application.Users.Interfaces
{
    public interface IAuthenticateService
    {
        Task<string> Authenticate(UserDto user);
    }
}
