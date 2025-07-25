using MediatR;
using RentalSystem.Application.DTOs;
using RentalSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalSystem.Application.Queries.GetAllRentals
{
    public class GetAllRentalsQueryHandler
         : IRequestHandler<GetAllRentalsQuery, List<GetAllRentalsDto>>
    {
        private readonly IRentalRepository _rentalRepository;

        public GetAllRentalsQueryHandler(IRentalRepository rentalRepository)
        { 
            _rentalRepository = rentalRepository;
        }

        public async Task<List<GetAllRentalsDto>> Handle(GetAllRentalsQuery query, CancellationToken cancellationToken)
        {
            return await _rentalRepository.GetAllAsync();
        }
    }
}
