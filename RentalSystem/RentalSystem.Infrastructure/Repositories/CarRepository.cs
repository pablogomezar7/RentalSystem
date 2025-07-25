using Microsoft.EntityFrameworkCore;
using RentalSystem.Application.DTOs;
using RentalSystem.Application.Interfaces;
using RentalSystem.Domain.Entities;
using RentalSystem.Infrastructure.Data;

public class CarRepository : ICarRepository
{
    private readonly AppDbContext _context;

    public CarRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Car?> GetByIdAsync(Guid carId)
    { 
        return await _context.Cars.FindAsync(carId);
    }

    public async Task<List<CarDto>> GetAllAsync()
    {
        return await _context.Cars.Select(c => new CarDto
        (
            c.Id,
            c.Type,
            c.Model
        )).ToListAsync();
    }


    public async Task<Car?> GetByTypeAndModelAsync(string carType, string carModel)
    {
        return await _context.Cars.FirstOrDefaultAsync(c => c.Type == carType && c.Model == carModel);
    }

    public async Task<Car?> FindAvailableCarAsync(string carType, string carModel, DateTime startDate, DateTime endDate)
    {

        return await _context.Cars
            .Include(c => c.Services) 
            .Where(c => c.Type == carType && c.Model == carModel)
            .Where(c =>
                !_context.Rentals.Any(r =>
                    r.Car.Id == c.Id &&
                    ((startDate >= r.StartDate && startDate <= r.EndDate) ||
                     (endDate >= r.StartDate && endDate <= r.EndDate))
                )
            )
            .FirstOrDefaultAsync();

    }

    public async Task<List<Car>> GetCarsWithServiceBetweenDatesAsync(DateTime fromDate, DateTime toDate)
    {
        return await _context.Cars
            .Include(c => c.Services)
            .Where(c => c.Services.Any(s =>
                s.Date >= fromDate && s.Date <= toDate))
            .ToListAsync();
    }

    public async Task<List<(Guid CarId, string Model, string Type, DateTime ServiceDate)>> GetCarsWithServicesInNextToWeeksAsync()
    { 
        var limitDate = DateTime.Today.AddDays(14);


        var data = await _context.Cars
            .SelectMany(c => c.Services
                            .Where(s => s.Date >= DateTime.Today && s.Date <= limitDate)
                            .Select(s => new 
                            {
                                CarId = c.Id,
                                Model = c.Model,
                                Type = c.Type,
                                ServiceDate = s.Date                                          
            })).ToListAsync();

        return data.Select(x => (x.CarId, x.Model, x.Type, x.ServiceDate)).ToList();

    }
}

