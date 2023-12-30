using AutoMapper;
using LeHotelMax.Application.Common.Enums;
using LeHotelMax.Application.Common.Models;
using LeHotelMax.Application.Hotels.Dtos;
using LeHotelMax.Application.Hotels.Interfaces;
using LeHotelMax.Application.Hotels.Query;
using MediatR;

namespace LeHotelMax.Application.Hotels.QueryHandlers
{
    internal class GetHotelByIdQueryHandler : IRequestHandler<GetHotelByIdQuery, OperationResult<HotelDto>>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public GetHotelByIdQueryHandler(IHotelRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<HotelDto>> Handle(GetHotelByIdQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<HotelDto>();

            var data = await _hotelRepository.GetHotelByIdAsync(request.Id, cancellationToken);

            if (data is null)
            {
                result.AddError(ErrorCode.NotFound,
                  string.Format(HotelErrorMessages.HotelNotFound, request.Id));
                return result;
            }

            var mapped = _mapper.Map<HotelDto>(data);

            result.Data = mapped;

            return result;
        }
    }
}
