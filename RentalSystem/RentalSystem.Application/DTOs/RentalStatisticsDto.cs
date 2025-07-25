using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalSystem.Application.DTOs
{
    public class RentalStatisticsDto
    {
        public string MostRentedCarType { get; set; } = string.Empty;
        public double UtilizationPercentage { get; set; }
    }
}
