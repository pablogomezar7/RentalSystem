using MediatR;

namespace RentalSystem.Application.Commands.RegisterCustomer;

public record RegisterCustomerCommand(string FullName, string Address) : IRequest<Guid>;