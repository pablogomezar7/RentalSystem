using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalSystem.Application.Commands.CancelRental
{
    public class CancelRentalCommandValidator : AbstractValidator<CancelRentalCommand>
    {
        public CancelRentalCommandValidator()
        {
            RuleFor(x => x.rentalId).NotEmpty();
        }
    }
}
