using MediatR;
using RentalSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalSystem.Application.Queries.GetCarUsageQuery
{
    public record GetCarUsageQuery(DateTime From, DateTime To) : IRequest<List<CarUsageDto>>;

}
