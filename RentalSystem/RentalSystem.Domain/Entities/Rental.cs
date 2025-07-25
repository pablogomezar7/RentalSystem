using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalSystem.Domain.Entities
{
    public class Rental
    {
        public Guid Id { get; set; }
        public Customer Customer { get; set; }
        public Guid CustomerId { get; set; }
        public Car Car { get; set; }
        public Guid CarId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsCanceled { get; set; }

        public Rental(){}

    }
}
