using AutoMapper;
using LeHotelMax.Application.Users.Command;
using LeHotelMax.Application.Users.Dtos;

namespace LeHotelMax.Application.Users.MappingProfiles
{
    public class UserProfiles: Profile
    {
        public UserProfiles()
        {
            CreateMap<LoginDto, LoginCommand>();
            CreateMap<LoginCommand, LoginDto>();
        }
    }
}
