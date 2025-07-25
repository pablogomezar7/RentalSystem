using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalSystem.Application.DTOs
{
    public class CarServiceDto
    {
        public Guid CarId { get; set; }
        public string Model {  get; set; }
        public string Type { get; set; }
        public DateTime ServiceDate {  get; set; }
    }
}
