using MediatR;
using RentalSystem.Application.DTOs;
using RentalSystem.Application.Interfaces;
using RentalSystem.Application.Queries.GetTopRentedCar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalSystem.Application.Queries.GetTopRentedCars
{
    public class GetTopRentedCarsQueryHandler
     : IRequestHandler<GetTopRentedCarsQuery, List<TopRentedCarsDto>>
    {
        private readonly IRentalRepository _rentalRepository;

        public GetTopRentedCarsQueryHandler(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        public async Task<List<TopRentedCarsDto>> Handle(GetTopRentedCarsQuery request, CancellationToken cancellationToken)
        {
            return await _rentalRepository.GetTopRentedCarsAsync(request.From, request.To);
        }
    }


}
