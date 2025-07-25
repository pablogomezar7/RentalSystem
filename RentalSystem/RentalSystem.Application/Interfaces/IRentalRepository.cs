using MediatR;
using RentalSystem.Application.DTOs;
using RentalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalSystem.Application.Interfaces
{
    public interface IRentalRepository
    {
        Task<Rental?> GetByIdAsync(Guid id);
        Task<List<GetAllRentalsDto?>> GetAllAsync();
        Task<RegisteredRentalDto> AddAsync(Rental rental);
        Task UpdateAsync(Rental rental);
        Task DeleteAsync(Guid id);
        Task<bool> HasOverlappingRentalAsync(Guid carId, DateTime startDate, DateTime endDate);
        Task CancelRentalAsync(Guid id);
        Task<List<TopRentedCarsDto>> GetTopRentedCarsAsync(DateTime from, DateTime to);
        Task<List<CarUsageDto>> GetCarUsageAsync(DateTime from, DateTime to);
        Task<List<CancellationRankingDto>> GetCancellationsRankingAsync(DateTime from, DateTime to);
        Task<List<DailyActivityDto>> GetDailyActivityAsync(DateTime from, DateTime to);
        Task<string> GetMostRentedCarTypeAsync();
        Task<double> GetUtilizationPercentageAsync(DateTime from, DateTime to);
    }
}
