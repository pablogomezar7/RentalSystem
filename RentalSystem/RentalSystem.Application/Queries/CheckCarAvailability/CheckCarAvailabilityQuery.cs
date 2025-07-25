using MediatR;
using Microsoft.EntityFrameworkCore;
using RentalSystem.Application.Commands.RegisterRental;
using RentalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalSystem.Application.Queries.CheckCarAvailability
{
    public record CheckCarAvailabilityQuery(
        string CarType,
        string CarModel,
        DateTime StartDate,
        DateTime EndDate
    ) : IRequest<bool>;

}
