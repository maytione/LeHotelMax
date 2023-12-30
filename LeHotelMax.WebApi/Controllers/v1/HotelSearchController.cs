using LeHotelMax.Application.Hotels.Query;
using LeHotelMax.WebApi.Dtos.Hotels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeHotelMax.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class HotelSearchController: BaseController
    {

        private readonly IMediator _mediator;

        public HotelSearchController( IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> SearchForHotels([FromQuery] SearchHotelsPaginated request, CancellationToken cancellationToken)
        {
            var query = new SearchForHotelsQuery() 
            { 
                Longitude = request.Longitude, 
                Latitude = request.Latitude,  
                PageNumber = request.PageNumber, 
                PageSize = request.PageSize 
            };
            var result = await _mediator.Send(query, cancellationToken);
            return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result.Data);
        }


    }
}
