using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalSystem.Application.DTOs
{
    public class RegisteredRentalDto
    {
        public CarDto Car { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        public RegisteredRentalDto(CarDto car, DateTime startDate, DateTime endDate)
        {
            Car = car;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
