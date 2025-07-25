using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalSystem.Application.Queries.GetAllCarsQuery;
using RentalSystem.Application.Queries.GetCarsWithUpcomingServices;
using RentalSystem.Application.Queries.GetTopRentedCar;

namespace RentalSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CarController(IMediator mediator )
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GeAllAsync()
        {
            var cars = await _mediator.Send(new GetAllCarsQuery());
            return Ok(cars);
        }

        [Authorize]
        [HttpGet("upcoming-services")]
        public async Task<IActionResult> GetCarsWithUpcomingServices()
        {
            var cars = await _mediator.Send(new GetCarsWithUpcomingServicesQuery());
            return Ok(cars);
        }

        [HttpGet("top-rented-cars")]
        public async Task<IActionResult> GetTopRentedCars([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            var result = await _mediator.Send(new GetTopRentedCarsQuery(from, to));
            return Ok(result);
        }
    }
}
