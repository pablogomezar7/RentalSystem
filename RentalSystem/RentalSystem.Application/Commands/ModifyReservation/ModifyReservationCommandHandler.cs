using MediatR;
using RentalSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalSystem.Application.Commands.ModifyReservation
{
    public class ModifyReservationCommandHandler : IRequestHandler<ModifyReservationCommand>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly ICarRepository _carRepository;

        public ModifyReservationCommandHandler(IRentalRepository rentalRepository, ICarRepository carRepository )
        {
            _rentalRepository = rentalRepository;
            _carRepository = carRepository;
        }

        public async Task Handle(ModifyReservationCommand request, CancellationToken cancellationToken)
        {
            var rental = await _rentalRepository.GetByIdAsync(request.RentalId);
            if (rental == null)
                throw new InvalidOperationException("rental doesn't exist");

            var car = await _carRepository.FindAvailableCarAsync(request.NewCarType, request.NewCarModel, request.NewStartDate, request.NewEndDate);
            if (car == null) 
                throw new InvalidOperationException("no avialable car with selected options");

            var hasOverlap = await _rentalRepository.HasOverlappingRentalAsync(car.Id, request.NewEndDate, request.NewEndDate);

            if (hasOverlap)
                throw new InvalidOperationException("car not available for selected dates");

            rental.StartDate = request.NewStartDate;
            rental.EndDate = request.NewEndDate;
            rental.Car = car;
            rental.CarId = car.Id;

            await _rentalRepository.UpdateAsync(rental);
        }
    }
}
