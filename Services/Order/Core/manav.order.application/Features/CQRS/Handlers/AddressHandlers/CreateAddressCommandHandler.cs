using manav.order.applicaiton.Features.CQRS.Commands.AddressComands;
using manav.order.applicaiton.Interfaces;
using manav.order.domain.Entities;

namespace manav.order.applicaiton.Features.CQRS.Handlers.AddressHandlers;

public class CreateAddressCommandHandler
{
    private readonly IRepository<Address> _repository;

    public CreateAddressCommandHandler(IRepository<Address> repository)
    {
        _repository = repository;
    }

    public async Task Handle(CreateAddressCommand createAddressCommand)
    {
        await _repository.CreateAsync(new Address
        {
            UserID = createAddressCommand.UserID,
            AdressDetails = createAddressCommand.AdressDetails,
            FirstName = createAddressCommand.FirstName,
            LastName = createAddressCommand.LastName,
            Email = createAddressCommand.Email,
            Number = createAddressCommand.Number
        });
    }
}