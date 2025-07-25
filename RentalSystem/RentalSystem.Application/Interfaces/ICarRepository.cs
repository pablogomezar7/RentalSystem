using RentalSystem.Application.DTOs;
using RentalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalSystem.Application.Interfaces
{
    public interface ICarRepository
    {
        Task<Car?> GetByIdAsync(Guid carId);
        Task<List<CarDto>> GetAllAsync();
        Task<Car?> FindAvailableCarAsync(string carType, string carModel, DateTime startDate, DateTime endDate);
        Task<Car?> GetByTypeAndModelAsync(string carType, string carModel);
        Task<List<Car>> GetCarsWithServiceBetweenDatesAsync(DateTime startDate, DateTime endDate);
        Task<List<(Guid CarId, string Model, string Type, DateTime ServiceDate)>> GetCarsWithServicesInNextToWeeksAsync();
    }
}
