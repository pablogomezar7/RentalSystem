using MediatR;
using RentalSystem.Application.DTOs;
using RentalSystem.Application.Interfaces;
using RentalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalSystem.Application.Commands.RegisterRental
{

    public class RegisterRentalCommandHandler : IRequestHandler<RegisterRentalCommand, RegisteredRentalDto>
    {
        private readonly ICarRepository _carRepository;
        private readonly IRentalRepository _rentalRepository;
        private readonly ICustomerRepository _customerRepository;

        public RegisterRentalCommandHandler(
            ICarRepository carRepository,
            IRentalRepository rentalRepository,
            ICustomerRepository customerRepository)
        {
            _carRepository = carRepository;
            _rentalRepository = rentalRepository;
            _customerRepository = customerRepository;
        }

        public RegisterRentalCommandHandler() { }


        public async Task<RegisteredRentalDto> Handle(RegisterRentalCommand request, CancellationToken cancellationToken)
        {
            //get or create customer
            var customer = await _customerRepository.GetByIdAsync(request.CustomerId);
            if (customer is null)
            {
                customer = new Customer(request.CustomerId, request.IdNumber, request.FullName, request.Address);
                await _customerRepository.AddAsync(customer);
            }

            //get car
            var availableCar = await _carRepository.FindAvailableCarAsync(
                request.CarType, request.CarModel, request.StartDate, request.EndDate);

            if (availableCar is null)
                throw new InvalidOperationException("No car available for the given criteria.");

            //create and save rental
            Rental rental = new Rental {
                Id = Guid.NewGuid(),
                Customer = customer,
                CustomerId = customer.Id,
                Car = availableCar,
                CarId = availableCar.Id,
                StartDate = request.StartDate,
                EndDate = request.EndDate
            };

            return await _rentalRepository.AddAsync(rental);

            
        }
    }


}
