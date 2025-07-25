using FluentAssertions;
using Moq;
using RentalSystem.Application.DTOs;
using RentalSystem.Application.Interfaces;
using RentalSystem.Application.Queries.GetTopRentedCar;
using RentalSystem.Application.Queries.GetTopRentedCars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalSystem.Tests.Application.Queries.GetTopRentedCars
{
    public class GetTopRentedCarsQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsTopRentedCarsList()
        {
            // Arrange
            var mockRepository = new Mock<IRentalRepository>();

            var expectedCars = new List<TopRentedCarsDto>
            {
                new TopRentedCarsDto("First Car", "SUV", 5),
                new TopRentedCarsDto("Second Car", "Sedan", 3),
                new TopRentedCarsDto("Third Car", "Hatchback", 2)
            };

            mockRepository
                .Setup(repo => repo.GetTopRentedCarsAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(expectedCars);

            var handler = new GetTopRentedCarsQueryHandler(mockRepository.Object);

            var query = new GetTopRentedCarsQuery(
                From: DateTime.Today.AddDays(-30),
                To: DateTime.Today
            );

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(3);
            result.Should().Contain(c => c.Model == "First Car" && c.RentalsCount == 5);
            result.Should().Contain(c => c.Model == "Second Car" && c.RentalsCount == 3);
            result.Should().Contain(c => c.Model == "Third Car" && c.RentalsCount == 2);

            mockRepository.Verify(
                repo => repo.GetTopRentedCarsAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>()),
                Times.Once);
        }
    }
}
