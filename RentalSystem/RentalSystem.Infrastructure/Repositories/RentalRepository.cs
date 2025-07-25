using Azure.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentalSystem.Application.DTOs;
using RentalSystem.Application.Interfaces;
using RentalSystem.Domain.Entities;
using RentalSystem.Infrastructure.Data;
using System.Runtime.CompilerServices;

namespace RentalSystem.Infrastructure.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly AppDbContext _context;

        public RentalRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetAllRentalsDto>> GetAllAsync()
        {
            return await _context.Rentals.Select(r => new GetAllRentalsDto(
                r.Id,
                r.Car.Type,
                r.Car.Model,
                r.StartDate,
                r.EndDate
            )).ToListAsync();
        }

        public async Task<RegisteredRentalDto> AddAsync(Rental rental)
        {
            await _context.Rentals.AddAsync(rental);
            await _context.SaveChangesAsync();

            var carDto = new CarDto(
                rental.Car.Id,
                rental.Car.Model,
                rental.Car.Type
            );

            var registeredRentalDto = new RegisteredRentalDto(
                carDto,
                rental.StartDate,
                rental.EndDate
            );

            return registeredRentalDto;
        }

        public async Task<Rental?> GetByIdAsync(Guid rentalId)
        {
            return await _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.Car)
                .FirstOrDefaultAsync(r => r.Id == rentalId);
        }

        public async Task UpdateAsync(Rental rental)
        {
            _context.Rentals.Update(rental);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid rentalId)
        {
            var rental = await _context.Rentals.FindAsync(rentalId);
            if (rental is not null)
            {
                _context.Rentals.Remove(rental);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> HasOverlappingRentalAsync(Guid carId, DateTime startDate, DateTime endDate)
        {
            return await _context.Rentals.AnyAsync(r => r.CarId == carId && r.StartDate <= startDate && r.EndDate <= endDate);
        }

        public async Task CancelRentalAsync(Guid rentalId)
        {
            var rental = await _context.Rentals.FindAsync(rentalId);
            if (rental is not null)
            { 
                rental.IsCanceled = true;
                await _context.SaveChangesAsync();
            }       
        }

        public async Task<List<TopRentedCarsDto>> GetTopRentedCarsAsync(DateTime from, DateTime to)
        {
            return await _context.Rentals 
                .Where(r => r.StartDate >= from && r.EndDate <= to && !r.IsCanceled)
                .GroupBy(r => new { r.Car.Model, r.Car.Type })
                .Select(g => new TopRentedCarsDto(
                    g.Key.Model,
                    g.Key.Type,
                    g.Count()
                ))
                .OrderByDescending(x => x.RentalsCount)
                .Take(3)
                .ToListAsync();
        }

        public async Task<List<CarUsageDto>> GetCarUsageAsync(DateTime from, DateTime to)
        {
            var totalDays = (to - from).TotalDays + 1;

            var cars = await _context.Cars
                .Select(car => new CarUsageDto(
                    car.Model,
                    car.Type,
                    _context.Rentals
                        .Where(r => r.CarId == car.Id && !r.IsCanceled)
                        .Select(r => new
                        {
                            Start = r.StartDate < from ? from : r.StartDate,
                            End = r.EndDate > to ? to : r.EndDate
                        })
                        .Sum(r => EF.Functions.DateDiffDay(r.Start, r.End) + 1) * 100 / totalDays
                ))
                .ToListAsync();

            return cars;
        }

        public async Task<List<CancellationRankingDto>> GetCancellationsRankingAsync(DateTime from, DateTime to)
        {
            return await _context.Rentals
                .Where(r => r.StartDate >= from && r.EndDate <= to && r.IsCanceled)
                .GroupBy(r => new { r.Car.Model, r.Car.Type })
                .Select(g => new CancellationRankingDto(
                    g.Key.Model,
                    g.Key.Type,
                    g.Count()
                ))
                .OrderByDescending(x => x.CancelledRentals)
                .ToListAsync();
        }

        public async Task<List<DailyActivityDto>> GetDailyActivityAsync(DateTime from, DateTime to)
        {
            var totalCars = await _context.Cars.CountAsync();

            var activities = await _context.Rentals
                .Where(r => r.StartDate <= to && r.EndDate >= from)
                .GroupBy(r => r.StartDate.Date)
                .Select(g => new DailyActivityDto(
                    g.Key,
                    g.Count(r => !r.IsCanceled),
                    g.Count(r => r.IsCanceled),
                    totalCars - g.Count(r => !r.IsCanceled)
                ))
                .ToListAsync();

            return activities;
        }

        public async Task<string> GetMostRentedCarTypeAsync() 
        {
            var rentals = await _context.Rentals
                .Where(r => !r.IsCanceled)
                .Include(c => c.Car)
                .ToListAsync();

            if (rentals.Count == 0)
                throw new InvalidOperationException("No rentals to show");
            
            return rentals.GroupBy(r => r.Car.Type).OrderByDescending(ct => ct.Count())
                .Select(ct => ct.Key)
                .FirstOrDefault() ?? "N/A";
        }

        public async Task<double> GetUtilizationPercentageAsync(DateTime dateFrom, DateTime dateTo)
        {
            var rentalsInRange = await _context.Rentals
            .Where(r => !r.IsCanceled &&
                r.StartDate <= dateTo &&
                r.EndDate >= dateFrom)
            .ToListAsync();

            var totalDaysRange = (dateTo - dateFrom).TotalDays + 1;
            var totalCars = await _context.Cars.CountAsync();
            var totalAvailableDays = totalDaysRange * totalCars;

            var rentedDays = rentalsInRange.Sum(r =>
            {
                var from = r.StartDate < dateFrom ? dateFrom : r.StartDate;
                var to = r.EndDate > dateTo ? dateTo : r.EndDate;
                return (to - from).TotalDays + 1;
            });

            var utilization = totalAvailableDays > 0
                ? Math.Round((rentedDays / totalAvailableDays) * 100, 2)
                : 0;

            return utilization;

        }


    }
}
