using manav.order.applicaiton.Features.CQRS.Commands.AddressComands;
using manav.order.applicaiton.Interfaces;
using manav.order.domain.Entities;

namespace manav.order.applicaiton.Features.CQRS.Handlers.AddressHandlers;

public class UpdateAddressCommandHandler
{
    private readonly IRepository<Address> _addressRepository;

    public UpdateAddressCommandHandler(IRepository<Address> addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public async Task Handle(UpdateAddressCommand request)
    {
        var values = await _addressRepository.GetByIdAsync(request.AdressID);
        values.AddressID = request.AdressID;
        values.UserID = request.UserID;
        await _addressRepository.UpdateAsync(values);
    }
}