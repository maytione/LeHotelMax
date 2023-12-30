using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


namespace LeHotelMax.Infrastructure.Data.Identity.Services
{
    internal class TokenGenerator : ITokenGenerator
    {
        public GenerateTokenResponse Generate(GenerateTokenRequest generateTokenRequest)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(generateTokenRequest.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken securityToken = new(generateTokenRequest.Issuer,
                generateTokenRequest.Audience,
                generateTokenRequest.Claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(generateTokenRequest.Expires),
                credentials);
            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return new GenerateTokenResponse(token!);
        }
    }
}
