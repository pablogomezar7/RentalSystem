using MediatR;
using RentalSystem.Application.DTOs;
using RentalSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalSystem.Application.Queries.GetCarsWithUpcomingServices
{
    public class GetCarsWithUpcomingServicesQueryHandler : IRequestHandler<GetCarsWithUpcomingServicesQuery, List<CarServiceDto>>
    {
        private readonly ICarRepository _carRepository;

        public GetCarsWithUpcomingServicesQueryHandler(ICarRepository carRepository)
        { 
            _carRepository = carRepository;
        }

        public async Task<List<CarServiceDto>> Handle(GetCarsWithUpcomingServicesQuery request, CancellationToken cancellationToken)
        {
            var upcomingServices = await _carRepository.GetCarsWithServicesInNextToWeeksAsync();
            
            return upcomingServices.Select(s => new CarServiceDto { 
                CarId = s.CarId,
                Model = s.Model,
                Type = s.Type,
                ServiceDate = s.ServiceDate
            }).ToList();

        }
    }
}
