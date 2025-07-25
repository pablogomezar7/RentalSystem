using Moq;
using RentalSystem.Application.DTOs;
using RentalSystem.Application.Interfaces;
using query = RentalSystem.Application.Queries.GetDailyActivityQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace RentalSystem.Tests.Application.Queries.GetDailyActivityQuery
{
    public class GetDailyActivityQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsDailyActivityList()
        {
            // Arrange
            var mockRepo = new Mock<IRentalRepository>();
            var expected = new List<DailyActivityDto>
        {
            new(DateTime.Today, 2, 1, 5),
            new(DateTime.Today.AddDays(1), 3, 0, 4)
        };

            mockRepo.Setup(r => r.GetDailyActivityAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(expected);

            var handler = new query.GetDailyActivityQueryHandler(mockRepo.Object);

            // Act
            var result = await handler.Handle(
                new query.GetDailyActivityQuery(DateTime.Today.AddDays(-7), DateTime.Today),
                CancellationToken.None);

            // Assert
            result.Should().HaveCount(2);
            result.Should().Contain(x => x.Date == DateTime.Today && x.Rentals == 2);
        }
    }

}
