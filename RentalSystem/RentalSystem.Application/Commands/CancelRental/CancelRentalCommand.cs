﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalSystem.Application.Commands.CancelRental
{
    public record CancelRentalCommand(Guid rentalId): IRequest;
}
