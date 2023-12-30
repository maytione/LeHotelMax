using LeHotelMax.Application.Users.Dtos;
using LeHotelMax.Application.Users.Interfaces;

namespace LeHotelMax.Infrastructure.Data.Identity.Services
{
    internal sealed class AuthenticateService : IAuthenticateService
    {
        private readonly IAccessTokenService _accessTokenService;
        public AuthenticateService(IAccessTokenService accessTokenService)
        {
            _accessTokenService = accessTokenService;
        }

        public Task<string> Authenticate(UserDto user)
        {
            var token = _accessTokenService.Generate(user);
            return Task.FromResult(token);
        }
    }
}
