
namespace LeHotelMax.WebApi.Dtos.Hotels
{
    public class SearchHotelsPaginated: PaginatedRequestBase
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
