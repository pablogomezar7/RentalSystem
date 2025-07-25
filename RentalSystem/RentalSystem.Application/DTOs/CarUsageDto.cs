using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalSystem.Application.DTOs
{
    public record CarUsageDto(string Model, string Type, double UsagePercentage);

}
