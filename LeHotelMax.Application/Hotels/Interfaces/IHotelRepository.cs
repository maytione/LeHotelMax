using LeHotelMax.Domain.Aggregates;
using LeHotelMax.Domain.Aggregates.ValueObjects;

namespace LeHotelMax.Application.Hotels.Interfaces
{
    public interface IHotelRepository
    {
        Task<(IEnumerable<Hotel> Hotels, int TotalCount)> GetAllHotelsAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
        Task<Hotel?> GetHotelByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Hotel?> GetHotelByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<Hotel> AddHotelAsync(Hotel hotel, CancellationToken cancellationToken = default);
        Task UpdateHotelAsync(Hotel hotel, CancellationToken cancellationToken = default);
        Task DeleteHotelAsync(Hotel hotel, CancellationToken cancellationToken = default);
        Task<(IEnumerable<HotelDistanceInfo> Hotels, int TotalCount)> GetHotelsOrderedByDistanceAndPrice(GeoLocation startLocation, int pageNumber, int pageSize);
    }
}
