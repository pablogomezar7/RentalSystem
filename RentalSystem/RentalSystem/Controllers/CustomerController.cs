using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentalSystem.Application.Commands;
using RentalSystem.Application.Commands.RegisterCustomer;

namespace RentalSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterCustomer(RegisterCustomerCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(RegisterCustomer), new { id }, null);
        }
    }

}
