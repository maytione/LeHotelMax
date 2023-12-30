using AutoMapper;
using LeHotelMax.Application.Common.Interfaces;
using LeHotelMax.Application.Common.Models;
using LeHotelMax.Application.Common.Pagination;
using LeHotelMax.Application.Hotels.Dtos;
using LeHotelMax.Application.Hotels.Interfaces;
using LeHotelMax.Application.Hotels.Query;
using MediatR;

namespace LeHotelMax.Application.Hotels.QueryHandlers
{
    internal class GetAllHotelsQueryHandler : IRequestHandler<GetAllHotelsQuery, OperationResult<IPaginatedList<HotelDto>>>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public GetAllHotelsQueryHandler(IHotelRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<IPaginatedList<HotelDto>>> Handle(GetAllHotelsQuery request, CancellationToken cancellationToken)
        {
            var data = await _hotelRepository.GetAllHotelsAsync(request.PageNumber, request.PageSize, cancellationToken);

            var mapped = _mapper.ProjectTo<HotelDto>(data.Hotels.AsQueryable()).ToList().AsReadOnly();

            PaginatedList<HotelDto> paginated = new(mapped, data.TotalCount, request.PageNumber, request.PageSize);

            return new OperationResult<IPaginatedList<HotelDto>>() { Data = paginated };
        }
    }
}
