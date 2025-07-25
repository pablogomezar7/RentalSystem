using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalSystem.Application.Commands.CancelRental;
using RentalSystem.Application.Commands.ModifyReservation;
using RentalSystem.Application.Commands.RegisterRental;
using RentalSystem.Application.Queries.CheckCarAvailability;
using RentalSystem.Application.Queries.GetAllRentals;
using RentalSystem.Application.Queries.GetCancellationsRankingQuery;
using RentalSystem.Application.Queries.GetCarsWithUpcomingServices;
using RentalSystem.Application.Queries.GetCarUsageQuery;
using RentalSystem.Application.Queries.GetDailyActivityQuery;
using RentalSystem.Application.Queries.GetRentalStatisticsQuery;
using RentalSystem.Application.Queries.GetTopRentedCar;
namespace RentalSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RentalController(IMediator mediator)
        {
            _mediator = mediator;

        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery]GetAllRentalsQuery query) 
        {
            var rentals = await _mediator.Send(query);
            return Ok(rentals);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RegisterRental([FromBody] RegisterRentalCommand command)
        {
            var rental = await _mediator.Send(command);
            return Ok(rental);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyRental(Guid id, [FromBody] ModifyReservationCommand request)
        {
            var command = new ModifyReservationCommand(
                id,
                request.NewStartDate,
                request.NewEndDate,
                request.NewCarType,
                request.NewCarModel
            );
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelRental(Guid id)
        {
            await _mediator.Send(new CancelRentalCommand(id));
            return NoContent();
        }

        [HttpGet("availability")]
        public async Task<IActionResult> CheckAvailability([FromQuery] CheckCarAvailabilityQuery query)
        {
            var available = await _mediator.Send(query);
            return Ok(available);
        }

        [HttpGet("top-rented-cars")]
        public async Task<IActionResult> GetTopRentedCars([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            var result = await _mediator.Send(new GetTopRentedCarsQuery(from, to));
            return Ok(result);
        }

        [HttpGet("car-usage")]
        public async Task<IActionResult> GetCarUsage([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            var result = await _mediator.Send(new GetCarUsageQuery(from, to));
            return Ok(result);
        }

        [HttpGet("cancellations-ranking")]
        public async Task<IActionResult> GetCancellationsRanking([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            var result = await _mediator.Send(new GetCancellationsRankingQuery(from, to));
            return Ok(result);
        }

        [HttpGet("daily-activity")]
        public async Task<IActionResult> GetDailyActivity([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            var result = await _mediator.Send(new GetDailyActivityQuery(from, to));
            return Ok(result);
        }

        [HttpGet("statistics")]
        public async Task<IActionResult> GetStatistics([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            var result = await _mediator.Send(new GetRentalStatisticsQuery(from, to));
            return Ok(result);
        }


    }
}
