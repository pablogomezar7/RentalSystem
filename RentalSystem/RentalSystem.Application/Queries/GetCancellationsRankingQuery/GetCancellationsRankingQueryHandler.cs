using MediatR;
using RentalSystem.Application.DTOs;
using RentalSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalSystem.Application.Queries.GetCancellationsRankingQuery
{
    public class GetCancellationsRankingQueryHandler
        : IRequestHandler<GetCancellationsRankingQuery, List<CancellationRankingDto>>
    {
        private readonly IRentalRepository _rentalRepository;

        public GetCancellationsRankingQueryHandler(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        public async Task<List<CancellationRankingDto>> Handle(GetCancellationsRankingQuery request, CancellationToken cancellationToken)
        {
            return await _rentalRepository.GetCancellationsRankingAsync(request.From, request.To);
        }
    }

}
