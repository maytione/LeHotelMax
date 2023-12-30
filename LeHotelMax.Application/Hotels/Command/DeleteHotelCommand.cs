using LeHotelMax.Application.Common.Models;
using MediatR;


namespace LeHotelMax.Application.Hotels.Command
{
    public class DeleteHotelCommand: IRequest<OperationResult<bool>>
    {
        public required int Id { get; set; }
    }
}
