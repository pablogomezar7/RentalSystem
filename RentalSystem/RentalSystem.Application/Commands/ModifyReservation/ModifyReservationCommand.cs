using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalSystem.Application.Commands.ModifyReservation
{
    public record ModifyReservationCommand (Guid RentalId, DateTime NewStartDate, DateTime NewEndDate, string NewCarType, string NewCarModel) : IRequest;
    
}
