using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalSystem.Application.DTOs
{
    public record GetAllRentalsDto(Guid Id, string CarType, string CarModel, DateTime From, DateTime To);
}
