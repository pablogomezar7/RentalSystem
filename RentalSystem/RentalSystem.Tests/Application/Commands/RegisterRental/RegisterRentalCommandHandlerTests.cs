using MediatR;
using Moq;
using RentalSystem.Application.Commands.RegisterRental;
using RentalSystem.Application.DTOs;
using RentalSystem.Application.Interfaces;
using RentalSystem.Application.Queries.CheckCarAvailability;
using RentalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalSystem.Tests.Application.Commands.RegisterRental
{
    public class RegisterRentalCommandHandlerTests
    {
        [Fact]
        public async Task Hanlde_ShouldCreateRental_WhenCarIsAvailable()
        {
            //Arrange
            var rentalRepoMock = new Mock<IRentalRepository>();
            var carRepoMock = new Mock<ICarRepository>();
            var customerRepoMock = new Mock<ICustomerRepository>();

            var customerId = Guid.NewGuid();
            var carId = Guid.NewGuid();

            var customer = new Customer(customerId, "123123", "Pablo Doe", "calle falsa 123");
            var servicesList = new List<Service>();
            var car = new Car { Id = carId, Type = "SUV", Model = "Toyota RAV4" };

            //customer and car exists
            customerRepoMock.Setup(r => r.GetByIdAsync(customerId, CancellationToken.None))
                .ReturnsAsync(customer);

            carRepoMock.Setup(c => c.GetByIdAsync(carId))
                .ReturnsAsync(car);

            //no overlap adding rental
           
            rentalRepoMock
                .Setup(r => r.HasOverlappingRentalAsync(It.IsAny<Guid>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(false);

            rentalRepoMock.Setup(r => r.AddAsync(It.IsAny<Rental>()))
                .ReturnsAsync(new RegisteredRentalDto(
                    new CarDto(carId, "Toyota RAV4", "SUV"),
                    DateTime.Today.AddMonths(3),
                    DateTime.Today.AddMonths(3).AddDays(3)
                ));



            var handler = new RegisterRentalCommandHandler(
                carRepoMock.Object,
                rentalRepoMock.Object,
                customerRepoMock.Object
                );

            var command = new RegisterRentalCommand(
                customerId,
                "123123",
                "Pablo G",
                "Calle falsa 123",
                DateTime.Today.AddMonths(2),
                DateTime.Today.AddMonths(2).AddDays(3),
                "Toyota RAV4",
                 "SUV"
            );


            //Act
            var rental = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.NotEqual(Guid.Empty, rental.Car.CarId);

            rentalRepoMock.Verify(r => r.AddAsync(It.Is<Rental>(r => 
                r.CustomerId == customerId &&
                r.CarId == carId &&
                r.StartDate == command.StartDate &&
                r.EndDate == command.EndDate 
            )), Times.Once);
        }

      /*  [Fact]
        public async Task Handle_ShouldThrowException_WhenCarDoesNotExist()
        {
            // Arrange
            var rentalRepoMock = new Mock<IRentalRepository>();
            var carRepoMock = new Mock<ICarRepository>();
            var customerRepoMock = new Mock<ICustomerRepository>();

            var customerId = Guid.NewGuid();

            var customer = new Customer(customerId, "Jane Smith", "456 Main St");

            //customer exsists
            customerRepoMock.Setup(r => r.GetByIdAsync(customerId))
                            .ReturnsAsync(customer);

            //car doesnt exsists
            carRepoMock.Setup(r => r.GetByTypeAndModelAsync("Sedan", "Honda Civic"))
                       .ReturnsAsync((Car?)null);

            var handler = new RegisterRentalCommandHandler(
                rentalRepoMock.Object,
                carRepoMock.Object,
                customerRepoMock.Object
            );

            var command = new RegisterRentalCommand(
                customerId,
                "Jane Smith",
                "456 Main St",
                DateTime.Today,
                DateTime.Today.AddDays(2),
                "Sedan",
                "Honda Civic"
            );

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                handler.Handle(command, CancellationToken.None));

            rentalRepoMock.Verify(r => r.AddAsync(It.IsAny<Rental>()), Times.Never);
        }
      */
    }
}
