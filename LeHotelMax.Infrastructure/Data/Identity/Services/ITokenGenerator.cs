using System.Security.Claims;


namespace LeHotelMax.Infrastructure.Data.Identity.Services
{
    public record GenerateTokenRequest(string SecretKey, string Issuer, string Audience, double Expires, IEnumerable<Claim>? Claims = null);

    public record GenerateTokenResponse(string Token);

    public interface ITokenGenerator
    {
        GenerateTokenResponse Generate(GenerateTokenRequest generateTokenRequest);
    }
}
