using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalSystem.Application.Commands.ModifyReservation
{
    public class ModifyReservationValidator : AbstractValidator<ModifyReservationCommand>
    {
        public ModifyReservationValidator()
        {
            RuleFor(x => x.RentalId).NotEmpty();
            RuleFor(x => x.NewStartDate).LessThan(x => x.NewEndDate);
            RuleFor(x => x.NewEndDate).GreaterThan(DateTime.Today);
            RuleFor(x => x.NewCarType).NotEmpty();
            RuleFor(x => x.NewCarModel).NotEmpty();
        }
    }

}
