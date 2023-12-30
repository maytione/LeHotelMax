using LeHotelMax.Application.Common.Interfaces;
using LeHotelMax.Application.Common.Models;
using LeHotelMax.Application.Hotels.Dtos;
using MediatR;

namespace LeHotelMax.Application.Hotels.Query
{
    public class GetAllHotelsQuery: IRequest<OperationResult<IPaginatedList<HotelDto>>>
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }
}
