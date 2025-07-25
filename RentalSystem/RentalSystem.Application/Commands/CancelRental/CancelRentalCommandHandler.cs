using MediatR;
using RentalSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalSystem.Application.Commands.CancelRental
{
    public class CancelRentalCommandHandler : IRequestHandler<CancelRentalCommand>
    {
        private readonly IRentalRepository _rentalRepository;

        public CancelRentalCommandHandler(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        public async Task Handle(CancelRentalCommand request, CancellationToken cancellationToken) 
        {
            var rental = await _rentalRepository.GetByIdAsync(request.rentalId);
            if (rental == null)
                throw new InvalidOperationException("rental doesn't exists.");

            if (rental.IsCanceled)
                throw new InvalidOperationException("rental is already canceled.");

            await _rentalRepository.CancelRentalAsync(rental.Id);
        }
    }
}
