using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalSystem.Domain.Entities
{
    public class Car
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Model { get; set; }
        public List<Service> Services { get; set; } = new();

        public Car() { }


    }

    
}
