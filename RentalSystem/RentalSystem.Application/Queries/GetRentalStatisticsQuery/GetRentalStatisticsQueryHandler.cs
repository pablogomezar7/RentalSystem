using MediatR;
using RentalSystem.Application.DTOs;
using RentalSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalSystem.Application.Queries.GetRentalStatisticsQuery
{
    public class GetRentalStatisticsQueryHandler : IRequestHandler<GetRentalStatisticsQuery, RentalStatisticsDto>
    {
        private readonly IRentalRepository _rentalRepository;

        public GetRentalStatisticsQueryHandler(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
           
        }

        public async Task<RentalStatisticsDto> Handle(GetRentalStatisticsQuery request, CancellationToken cancellationToken)
        {

            var mostRentedCarType = await _rentalRepository.GetMostRentedCarTypeAsync();
            var utilizationPercentage = await _rentalRepository.GetUtilizationPercentageAsync(request.From, request.To);

            return new RentalStatisticsDto
            {
                MostRentedCarType = mostRentedCarType,
                UtilizationPercentage = utilizationPercentage
            };
        }
    }

}
