using LeHotelMax.Application.Common.Models;
using LeHotelMax.Application.Users.Dtos;
using MediatR;

namespace LeHotelMax.Application.Users.Command
{
    public class LoginCommand : IRequest<OperationResult<UserDto>>
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
