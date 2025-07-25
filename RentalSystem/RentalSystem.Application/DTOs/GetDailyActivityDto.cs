using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalSystem.Application.DTOs
{
    public record DailyActivityDto(DateTime Date, int Rentals, int Cancellations, int AvailableCars);

}
