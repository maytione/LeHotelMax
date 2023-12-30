
namespace LeHotelMax.Application.Hotels.Dtos
{
    public class HotelDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public GeoLocationDto? GeoLocation { get; set; }

    }
}
