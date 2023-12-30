using LeHotelMax.Application.Common.Models;
using LeHotelMax.Application.Hotels.Dtos;
using MediatR;

namespace LeHotelMax.Application.Hotels.Query
{
    public class GetHotelByIdQuery:IRequest<OperationResult<HotelDto>>
    {
        public int Id { get; set; }
    }
}
