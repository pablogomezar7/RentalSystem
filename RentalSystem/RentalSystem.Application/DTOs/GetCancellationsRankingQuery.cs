using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalSystem.Application.DTOs
{
    public record CancellationRankingDto(string Model, string Type, int CancelledRentals);

}
