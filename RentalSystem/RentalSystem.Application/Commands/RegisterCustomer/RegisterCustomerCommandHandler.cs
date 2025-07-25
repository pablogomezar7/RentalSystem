using MediatR;
using RentalSystem.Application.Interfaces;
using RentalSystem.Domain.Entities;

namespace RentalSystem.Application.Commands.RegisterCustomer;

public class RegisterCustomerCommandHandler : IRequestHandler<RegisterCustomerCommand, Guid>
{
    private readonly ICustomerRepository _repository;

    public RegisterCustomerCommandHandler(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            FullName = request.FullName,
            Address = request.Address
        };

        await _repository.AddAsync(customer, cancellationToken);
        return customer.Id;
    }
}