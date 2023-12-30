using AutoMapper;
using LeHotelMax.Application.Common.Enums;
using LeHotelMax.Application.Common.Models;
using LeHotelMax.Application.Hotels.Command;
using LeHotelMax.Application.Hotels.Interfaces;
using MediatR;

namespace LeHotelMax.Application.Hotels.CommandHandlers
{
    internal class UpdateHotelCommandHandler : IRequestHandler<UpdateHotelCommand, OperationResult<bool>>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public UpdateHotelCommandHandler(IHotelRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<bool>> Handle(UpdateHotelCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<bool>();

            try
            {
                // Search for existing hotel with required ID
                var data = await _hotelRepository.GetHotelByIdAsync(request.Id, cancellationToken);

                // If not found return coresponding error message
                if (data is null)
                {
                    result.AddError(ErrorCode.NotFound,
                      string.Format(HotelErrorMessages.HotelNotFound, request.Id));
                    return result;
                }

                // Search for existing hotel with required name 
                var dataSameName = await _hotelRepository.GetHotelByNameAsync(request.Name!, cancellationToken);

                // Check for unique name rule (InMemory database not respecting any indexes) :(
                if (dataSameName is not null && dataSameName.Id != request.Id)
                {
                    result.AddError(ErrorCode.ValidationError,
                     string.Format(HotelErrorMessages.HotelAlreadyExist, request.Name));
                    return result;
                }

                // map updates on existing hotel
                var hotel = _mapper.Map(request, data);

                // execute update
                await _hotelRepository.UpdateHotelAsync(hotel!, cancellationToken);

                // return ack
                result.Data = true;
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
