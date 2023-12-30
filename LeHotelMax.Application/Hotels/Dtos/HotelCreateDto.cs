
namespace LeHotelMax.Application.Hotels.Dtos
{
    public class HotelCreateDto
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public GeoLocationDto? GeoLocation { get; set; }
    }
}
