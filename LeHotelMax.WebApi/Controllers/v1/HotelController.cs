using AutoMapper;
using LeHotelMax.Application.Hotels.Command;
using LeHotelMax.Application.Hotels.Dtos;
using LeHotelMax.Application.Hotels.Query;
using LeHotelMax.WebApi.Dtos.Hotels;
using LeHotelMax.WebApi.Filters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeHotelMax.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class HotelController: BaseController
    {

        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public HotelController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllHotels([FromQuery] HotelsPaginated request, CancellationToken cancellationToken)
        {
            var query = new GetAllHotelsQuery() { PageNumber = request.PageNumber, PageSize = request.PageSize };
            var result = await _mediator.Send(query, cancellationToken);
            return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result.Data);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var query = new GetHotelByIdQuery() { Id = id };
            var result = await _mediator.Send(query, cancellationToken);
            return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result.Data);
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize]
        [ValidateModel]
        public async Task<IActionResult> UpdateHotel(int id, [FromBody] HotelDto hotel, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return HandleModelStateError(ModelState);
            }
            if (id != hotel.Id)
            {
                return HandleErrorResponse("ID in the request body does not match the route parameter.");
            }
            var command = _mapper.Map<UpdateHotelCommand>(hotel);
            var result = await _mediator.Send(command, cancellationToken);
            return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result.Data);
        }

        [HttpPost]
        [Authorize]
        [ValidateModel]
        public async Task<IActionResult> CreateHotel([FromBody] HotelCreateDto hotel, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return HandleModelStateError(ModelState);
            }
            var command = _mapper.Map<CreateHotelCommand>(hotel);
            var result = await _mediator.Send(command, cancellationToken);
            return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result.Data);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        [ValidateModel]
        public async Task<IActionResult> DeleteHotel(int id, CancellationToken cancellationToken)
        {
            var command = new DeleteHotelCommand() { Id = id };
            var result = await _mediator.Send(command, cancellationToken);
            return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result.Data);
        }
    }
}
