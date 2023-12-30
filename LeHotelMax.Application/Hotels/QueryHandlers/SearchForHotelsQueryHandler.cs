using AutoMapper;
using LeHotelMax.Application.Common.Interfaces;
using LeHotelMax.Application.Common.Models;
using LeHotelMax.Application.Common.Pagination;
using LeHotelMax.Application.Hotels.Dtos;
using LeHotelMax.Application.Hotels.Interfaces;
using LeHotelMax.Application.Hotels.Query;
using LeHotelMax.Domain.Aggregates.ValueObjects;
using MediatR;

namespace LeHotelMax.Application.Hotels.QueryHandlers
{
    internal class SearchForHotelsQueryHandler : IRequestHandler<SearchForHotelsQuery, OperationResult<IPaginatedList<HotelDistanceDto>>>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public SearchForHotelsQueryHandler(IHotelRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<IPaginatedList<HotelDistanceDto>>> Handle(SearchForHotelsQuery request, CancellationToken cancellationToken)
        {
            var data = await _hotelRepository.GetHotelsOrderedByDistanceAndPrice(new GeoLocation(request.Latitude, request.Longitude), request.PageNumber, request.PageSize);

            var mapped = _mapper.ProjectTo<HotelDistanceDto>(data.Hotels.AsQueryable()).ToList().AsReadOnly();

            PaginatedList<HotelDistanceDto> paginated = new(mapped, data.TotalCount, request.PageNumber, request.PageSize);

            return new OperationResult<IPaginatedList<HotelDistanceDto>>() { Data = paginated };

        }
    }
}
