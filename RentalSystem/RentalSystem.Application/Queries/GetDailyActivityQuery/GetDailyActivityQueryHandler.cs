using MediatR;
using RentalSystem.Application.DTOs;
using RentalSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalSystem.Application.Queries.GetDailyActivityQuery
{
    public class GetDailyActivityQueryHandler
    : IRequestHandler<GetDailyActivityQuery, List<DailyActivityDto>>
    {
        private readonly IRentalRepository _rentalRepository;

        public GetDailyActivityQueryHandler(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        public async Task<List<DailyActivityDto>> Handle(GetDailyActivityQuery request, CancellationToken cancellationToken)
        {
            return await _rentalRepository.GetDailyActivityAsync(request.From, request.To);
        }
    }

}
