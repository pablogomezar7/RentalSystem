using MediatR;
using Microsoft.EntityFrameworkCore;
using RentalSystem.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RentalSystem.Application.Queries.CheckCarAvailability
{
    public class CheckCarAvailabilityQueryHandler : IRequestHandler<CheckCarAvailabilityQuery, bool>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly ICarRepository _carRepository;

        public CheckCarAvailabilityQueryHandler(IRentalRepository rentalRepository, ICarRepository carRepository)
        {
            _rentalRepository = rentalRepository;
            _carRepository = carRepository;
        }

        public async Task<bool> Handle(CheckCarAvailabilityQuery request, CancellationToken cancellationToken)
        {
            var car = await _carRepository.GetByTypeAndModelAsync(request.CarType, request.CarModel);
            if (car == null)
                return false;

            var hasOverlap = await _rentalRepository.HasOverlappingRentalAsync(
                car.Id, request.StartDate, request.EndDate);
            return !hasOverlap;
        }
    }
}
