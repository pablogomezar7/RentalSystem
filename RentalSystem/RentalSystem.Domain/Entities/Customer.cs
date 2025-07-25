using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalSystem.Domain.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string IdNumber { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }

        public Customer() { }

        public Customer(Guid id, string idNumber, string fullName, string address)
        {
            Id = id;
            IdNumber = idNumber;
            FullName = fullName;
            Address = address;
        }

    }
}
