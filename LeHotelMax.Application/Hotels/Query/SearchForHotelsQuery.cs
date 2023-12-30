using LeHotelMax.Application.Common.Interfaces;
using LeHotelMax.Application.Common.Models;
using LeHotelMax.Application.Hotels.Dtos;
using MediatR;

namespace LeHotelMax.Application.Hotels.Query
{
    public class SearchForHotelsQuery: GeoLocationDto, IRequest<OperationResult<IPaginatedList<HotelDistanceDto>>>
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;

    }
}
