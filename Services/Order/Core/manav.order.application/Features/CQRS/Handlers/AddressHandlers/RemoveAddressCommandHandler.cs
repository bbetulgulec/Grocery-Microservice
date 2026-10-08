using manav.order.applicaiton.Features.CQRS.Commands.AddressComands;
using manav.order.applicaiton.Interfaces;
using manav.order.domain.Entities;

namespace manav.order.applicaiton.Features.CQRS.Handlers.AddressHandlers;

public class RemoveAddressCommandHandler
{
    
    private readonly IRepository<Address> _addressRepository;
    public RemoveAddressCommandHandler(IRepository<Address> addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public async Task Handle(RemoveAddressCommand request)
    {
        var value = await _addressRepository.GetByIdAsync(request.Id);
        await _addressRepository.DeleteAsync(value);
    }
}