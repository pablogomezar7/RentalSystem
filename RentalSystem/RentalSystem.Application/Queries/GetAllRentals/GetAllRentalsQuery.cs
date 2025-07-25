using MediatR;
using RentalSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalSystem.Application.Queries.GetAllRentals
{
    public record GetAllRentalsQuery() : IRequest<List<GetAllRentalsDto>>;
}
