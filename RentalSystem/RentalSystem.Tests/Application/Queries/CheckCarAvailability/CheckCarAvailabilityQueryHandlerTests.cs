//using Moq;
//using RentalSystem.Application.Interfaces;
//using RentalSystem.Application.Queries.CheckCarAvailability;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RentalSystem.Tests.Application.Queries.CheckCarAvailability
//{
//    public class CheckCarAvailabilityQueryHandlerTests
//    {
//        [Fact]
//        public async Task Handle_ReturnsTrue_WhenCarIsAvailable() 
//        {
//            //Arrange
//            var rentalRepoMock = new Mock<IRentalRepository>();
//            rentalRepoMock.Setup(r => r.HasOverlappingRentalAsync(It.IsAny<Guid>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
//            .ReturnsAsync(false);

//            var handler = new CheckCarAvailabilityQueryHandler(rentalRepoMock.Object);

//            var query = new CheckCarAvailabilityQuery
//            {
//                CarType = "Car type",
//                CarModel = "car model",
//                StartDate = DateTime.Today,
//                EndDate = DateTime.Today.AddDays(3),
//            };

//            //Act
//            var result = await handler.Handle(query, CancellationToken.None);

//            //Assert
//            Assert.True(result);
//        }

//        [Fact]
//        public async Task Handle_ReturnsFalse_WhenCarIsNotAvailable() 
//        {
//            //Arrange
//            var rentalRepoMock = new Mock<IRentalRepository>();
//            rentalRepoMock.Setup(r => r.HasOverlappingRentalAsync(It.IsAny<Guid>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
//            .ReturnsAsync(true);

//            var handler = new CheckCarAvailabilityQueryHandler(rentalRepoMock.Object);

//            var query = new CheckCarAvailabilityQuery
//            {
//                CarId = Guid.NewGuid(),
//                StartDate = DateTime.Today,
//                EndDate = DateTime.Today.AddDays(3),
//            };

//            //Act
//            var result = await handler.Handle(query, CancellationToken.None);

//            //Assert
//            Assert.False(result);
//        }
//    }
//}
