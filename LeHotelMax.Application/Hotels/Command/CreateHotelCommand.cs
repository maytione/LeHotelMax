using LeHotelMax.Application.Common.Models;
using LeHotelMax.Application.Hotels.Dtos;
using MediatR;

namespace LeHotelMax.Application.Hotels.Command
{
    public class CreateHotelCommand:HotelDto, IRequest<OperationResult<HotelDto>>
    {
    }
}
