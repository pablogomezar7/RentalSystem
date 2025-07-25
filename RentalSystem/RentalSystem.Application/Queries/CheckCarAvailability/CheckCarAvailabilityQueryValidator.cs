using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalSystem.Application.Queries.CheckCarAvailability
{
    public class CheckCarAvailabilityValidator : AbstractValidator<CheckCarAvailabilityQuery>
    {
        public CheckCarAvailabilityValidator()
        {
            RuleFor(x => x.CarType).NotEmpty();
            RuleFor(x => x.CarModel).NotEmpty();
            RuleFor(x => x.StartDate).LessThan(x => x.EndDate);
        }
    }
}
