using LeHotelMax.Application.Hotels.Interfaces;
using LeHotelMax.Domain.Aggregates;
using LeHotelMax.Domain.Aggregates.ValueObjects;
using Microsoft.EntityFrameworkCore;


namespace LeHotelMax.Infrastructure.Data.Repository
{
    public class HotelRepository: IHotelRepository
    {
        private readonly ApplicationDbContext _context; 

        public HotelRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Hotel> Hotels, int TotalCount)> GetAllHotelsAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            var query = _context.Hotels.Include(h => h.GeoLocation).AsQueryable();

            var count = await query.CountAsync(cancellationToken);

            var result = await query
                .OrderBy(item => item.Created)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (result, count);
        }

        public async Task<Hotel?> GetHotelByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Hotels.FindAsync([id, cancellationToken], cancellationToken: cancellationToken);
        }

        public async Task<Hotel?> GetHotelByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Hotels
            .FirstOrDefaultAsync(h => h.Name.Equals(name, StringComparison.OrdinalIgnoreCase), cancellationToken);

        }

        public async Task<Hotel> AddHotelAsync(Hotel hotel, CancellationToken cancellationToken = default)
        {
            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync(cancellationToken);
            return hotel;
        }

        public async Task UpdateHotelAsync(Hotel hotel, CancellationToken cancellationToken = default)
        {
            _context.Update(hotel);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteHotelAsync(Hotel hotel, CancellationToken cancellationToken = default)
        {
            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<(IEnumerable<HotelDistanceInfo> Hotels, int TotalCount)> GetHotelsOrderedByDistanceAndPrice(GeoLocation startLocation, int pageNumber, int pageSize)
        {

            var hotels = _context.Hotels.Include(h => h.GeoLocation).ToList();

            var sortedHotels = hotels
            .Select(hotel => new HotelDistanceInfo
            {
                Distance = CalculateDistance(startLocation, hotel.GeoLocation),
                Hotel = hotel
            })
            .OrderBy(info => Tuple.Create(info.Distance, info.Hotel!.Price))
            .ToList();

            var result = sortedHotels.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return await Task.FromResult((result, hotels.Count));
        }

        /// <summary>
        /// Haversine formula - CalculateDistance
        /// </summary>
        /// <param name="location1"></param>
        /// <param name="location2"></param>
        /// <returns>will return the distance in kilometers between those two points based on the Haversine formula</returns>
        private static double CalculateDistance(GeoLocation location1, GeoLocation location2)
        {

            const double earthRadiusKm = 6371.0; // Earth's radius in kilometers

            double lat1 = ToRadians(location1.Latitude);
            double lon1 = ToRadians(location1.Longitude);
            double lat2 = ToRadians(location2.Latitude);
            double lon2 = ToRadians(location2.Longitude);

            double dLat = lat2 - lat1;
            double dLon = lon2 - lon1;

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(lat1) * Math.Cos(lat2) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return earthRadiusKm * c; // Distance in kilometers
        }

        private static double ToRadians(double angle)
        {
            return angle * (Math.PI / 180);
        }

     
    }
}
