using LeHotelMax.Application.Users.Dtos;

namespace LeHotelMax.Infrastructure.Data.Identity.Services
{
    public interface ITokenService
    {
        string Generate(UserDto user);
    }
}
