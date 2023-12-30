
namespace LeHotelMax.Infrastructure.Data.Identity.Services
{
    public class JwtSettings
    {
        public required string Secret { get; set; }
        public double AccessTokenExpirationMinutes { get; set; }
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
    }
}
