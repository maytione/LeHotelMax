using LeHotelMax.Application.Users.Dtos;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace LeHotelMax.Infrastructure.Data.Identity.Services
{
    internal sealed class AccessTokenService : IAccessTokenService
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly JwtSettings _jwtSettings;

        public AccessTokenService(ITokenGenerator tokenGenerator, IOptions<JwtSettings> jwtSettings) =>
            (_tokenGenerator, _jwtSettings) = (tokenGenerator, jwtSettings.Value);

        public string Generate(UserDto user)
        {
            List<Claim> claims =
            [
                new Claim(ClaimTypes.Email, user.UserName!),
                new Claim(ClaimTypes.Name, user.UserName!),
                new ("IdentityId", user.Id!),
            ];
            return _tokenGenerator.Generate(new GenerateTokenRequest(_jwtSettings.Secret, _jwtSettings.Issuer,
                _jwtSettings.Audience,
                _jwtSettings.AccessTokenExpirationMinutes, claims)).Token;
        }
    }
}
