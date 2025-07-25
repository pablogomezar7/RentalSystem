using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentalSystem.Application.DTOs;

namespace RentalSystem.Application.Queries.GetCarsWithUpcomingServices
{
    public record GetCarsWithUpcomingServicesQuery : IRequest<List<CarServiceDto>>;
}
