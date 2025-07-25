using MediatR;
using RentalSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalSystem.Application.Commands.RegisterRental
{
    public record RegisterRentalCommand(Guid CustomerId, string IdNumber,string FullName, string Address, DateTime StartDate, DateTime EndDate, string CarModel, string CarType ): IRequest<RegisteredRentalDto>;
}
