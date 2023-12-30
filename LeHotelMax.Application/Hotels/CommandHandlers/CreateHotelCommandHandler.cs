using AutoMapper;
using LeHotelMax.Application.Common.Enums;
using LeHotelMax.Application.Common.Models;
using LeHotelMax.Application.Hotels.Command;
using LeHotelMax.Application.Hotels.Dtos;
using LeHotelMax.Application.Hotels.Interfaces;
using MediatR;

namespace LeHotelMax.Application.Hotels.CommandHandlers
{
    internal class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand, OperationResult<HotelDto>>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public CreateHotelCommandHandler(IHotelRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<HotelDto>> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<HotelDto>();

            try
            {
                // Search for existing hotel with required ID
                var data = await _hotelRepository.GetHotelByNameAsync(request.Name!, cancellationToken);

                // If not found return coresponding error message
                if (data is not null)
                {
                    result.AddError(ErrorCode.ValidationError,
                      string.Format(HotelErrorMessages.HotelAlreadyExist, request.Name));
                    return result;
                }

                // create new hotel from request
                var hotel = _mapper.Map(request, data) ?? throw new Exception("Something went wrong");

                // execute update
                var newHotel = await _hotelRepository.AddHotelAsync(hotel, cancellationToken);

                // return new hotel
                result.Data = _mapper.Map<HotelDto>(newHotel);
            }
            catch (Exception ex)
            {
                // catch any error and map to our error response
                result.AddError(ErrorCode.UpdateError,
                      string.Format(HotelErrorMessages.HotelUpdateError, request.Id, ex.Message));
            }

            return result;
        }
    }
}
