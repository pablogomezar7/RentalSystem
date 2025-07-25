using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentalSystem.Application.Queries.CheckCarAvailability;

namespace RentalSystem.Application.Commands.RegisterRental
{
    public class RegisterRentalCommandValidator : AbstractValidator<RegisterRentalCommand>
    {
        private readonly IMediator _mediator;

        public RegisterRentalCommandValidator(IMediator mediator) 
        {
            _mediator = mediator;

            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.FullName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Address).NotEmpty().MaximumLength(100);
            RuleFor(x => x.StartDate).NotEmpty().GreaterThanOrEqualTo(DateTime.Today);
            RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate);
            RuleFor(x => x.CarType).NotEmpty();
            RuleFor(x => x.CarModel).NotEmpty();
            RuleFor(x => x).MustAsync(CheckCarAvailability)
                        .WithMessage("Car not available ");
        }

        private async Task<bool> CheckCarAvailability(RegisterRentalCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new CheckCarAvailabilityQuery(
                command.CarType,
                command.CarModel,
                command.StartDate,
                command.EndDate

            ));
        }
    }
}
