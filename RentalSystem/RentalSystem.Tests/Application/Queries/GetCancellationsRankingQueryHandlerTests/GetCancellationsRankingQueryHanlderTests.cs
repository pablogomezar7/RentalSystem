using FluentAssertions;
using Moq;
using RentalSystem.Application.DTOs;
using RentalSystem.Application.Interfaces;
using RentalSystem.Application.Queries.GetCancellationsRankingQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalSystem.Tests.Application.Queries.GetCancellationsRankingQueryHandlerTests
{
    public class GetCancellationsRankingQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsRankingOfCancellations()
        {
            // Arrange
            var mockRepo = new Mock<IRentalRepository>();
            var expected = new List<CancellationRankingDto>
        {
            new("Toyota RAV4", "SUV", 3),
            new("Honda Accord", "Sedan", 1)
        };

            mockRepo.Setup(r => r.GetCancellationsRankingAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(expected);

            var handler = new GetCancellationsRankingQueryHandler(mockRepo.Object);

            // Act
            var result = await handler.Handle(
                new GetCancellationsRankingQuery(DateTime.Today.AddDays(-30), DateTime.Today),
                CancellationToken.None);

            // Assert
            result.Should().HaveCount(2);
            result.Should().Contain(x => x.Model == "Toyota RAV4" && x.CancelledRentals == 3);
        }
    }

}
