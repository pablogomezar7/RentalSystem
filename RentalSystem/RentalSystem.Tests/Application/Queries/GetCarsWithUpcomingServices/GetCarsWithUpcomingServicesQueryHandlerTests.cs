using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using RentalSystem.Application.Interfaces;
using RentalSystem.Application.Queries.GetCarsWithUpcomingServices;
using RentalSystem.Domain.Entities;
using RentalSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalSystem.Tests.Application.Queries.GetCarsWithUpcomingServices
{
    public class GetCarsWithUpcomingServicesQueryHandlerTests
    {
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
                .Options;

            var context = new AppDbContext(options);

            var car1 = new Car
            {
                Id = Guid.NewGuid(),
                Type = "SUV",
                Model = "Toyota RAV4 with next service in 5 days",
                Services = new List<Service>
                {
                    new Service { Date = DateTime.Today.AddDays(5) }, 
                    new Service { Date = DateTime.Today.AddDays(30) }
                }
            };

            var car2 = new Car
            {
                Id = Guid.NewGuid(),
                Type = "Sedan",
                Model = "Honda Accord With Next Service in 10 days",
                Services = new List<Service>
                {
                    new Service { Date = DateTime.Today.AddDays(10) }
                }
            };

            var car3 = new Car
            {
                Id = Guid.NewGuid(),
                Type = "Hatchback",
                Model = "VW Golf with next service in 20 days",
                Services = new List<Service>
                {
                    new Service { Date = DateTime.Today.AddDays(20) }
                }
            };

            context.Cars.AddRange(car1, car2, car3);
            context.SaveChanges();

            return context;
        }

        [Fact]
        public async Task Handle_ReturnsCarsWithServicesInNextTwoWeeks()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var carRepository = new CarRepository(dbContext);
            var handler = new GetCarsWithUpcomingServicesQueryHandler(carRepository);

            var query = new GetCarsWithUpcomingServicesQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);

            result.Should().Contain(c => c.Model == "Toyota RAV4 with next service in 5 days");
            result.Should().Contain(c => c.Model == "Honda Accord With Next Service in 10 days");
            result.Should().NotContain(c => c.Model == "VW Golf with next service in 20 days");
        }
    }
}
