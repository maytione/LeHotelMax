using LeHotelMax.Domain.Aggregates.ValueObjects;
using LeHotelMax.Domain.Common;

namespace LeHotelMax.Domain.Aggregates
{
    public class Hotel: BaseAuditable
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public required GeoLocation GeoLocation { get; set; }

    }
}
