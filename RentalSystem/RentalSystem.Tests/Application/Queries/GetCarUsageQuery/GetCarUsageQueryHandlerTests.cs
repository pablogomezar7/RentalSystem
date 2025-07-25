using FluentAssertions;
using Moq;
using RentalSystem.Application.DTOs;
using RentalSystem.Application.Interfaces;
using RentalSystem.Application.Queries.GetCarUsageQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalSystem.Tests.Application.Queries.GetCarUsageQueryHandler
{
    public class GetCarUsageQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsCarUsageList()
        {
            // Arrange
            var mockRepo = new Mock<IRentalRepository>();
            var expected = new List<CarUsageDto>
            {
                new("Toyota RAV4", "SUV", 80.0),
                new("Honda Accord", "Sedan", 50.0)
            };

            mockRepo.Setup(r => r.GetCarUsageAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(expected);

            var handler = new RentalSystem.Application.Queries.GetCarUsageQuery.GetCarUsageQueryHandler(mockRepo.Object);

            // Act
            var result = await handler.Handle(
                new GetCarUsageQuery(DateTime.Today.AddDays(-30), DateTime.Today),
                CancellationToken.None);

            // Assert
            result.Should().HaveCount(2);
            result.Should().Contain(x => x.Model == "Toyota RAV4" && x.UsagePercentage == 80.0);
        }
    }
}


