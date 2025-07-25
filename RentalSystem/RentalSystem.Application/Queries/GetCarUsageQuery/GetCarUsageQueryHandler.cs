using MediatR;
using RentalSystem.Application.DTOs;
using RentalSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalSystem.Application.Queries.GetCarUsageQuery
{
    public class GetCarUsageQueryHandler : IRequestHandler<GetCarUsageQuery, List<CarUsageDto>>
    {
        private readonly IRentalRepository _rentalRepository;

        public GetCarUsageQueryHandler(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        public async Task<List<CarUsageDto>> Handle(GetCarUsageQuery request, CancellationToken cancellationToken)
        {
            return await _rentalRepository.GetCarUsageAsync(request.From, request.To);
        }
    }
}
