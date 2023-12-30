using LeHotelMax.Application.Common.Enums;
using LeHotelMax.Application.Common.Models;
using LeHotelMax.Application.Hotels.Command;
using LeHotelMax.Application.Hotels.Interfaces;
using MediatR;

namespace LeHotelMax.Application.Hotels.CommandHandlers
{
    internal class DeleteHotelCommandHandler : IRequestHandler<DeleteHotelCommand, OperationResult<bool>>
    {
        private readonly IHotelRepository _hotelRepository;

        public DeleteHotelCommandHandler(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task<OperationResult<bool>> Handle(DeleteHotelCommand request, CancellationToken cancellationToken)
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
              
                // execute delete
                await _hotelRepository.DeleteHotelAsync(data, cancellationToken);

                // return ack
                result.Data = true;
            }
            catch (Exception ex)
            {
                // catch any error and map to our error response
                result.AddError(ErrorCode.DeleteError,
                      string.Format(HotelErrorMessages.HotelDeleteError, request.Id, ex.Message));
            }

            return result;
        }
    }
}
