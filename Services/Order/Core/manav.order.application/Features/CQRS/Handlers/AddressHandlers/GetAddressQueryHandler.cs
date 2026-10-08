using manav.order.applicaiton.Features.CQRS.Result.AddressResult;
using manav.order.applicaiton.Interfaces;
using manav.order.domain.Entities;

namespace manav.order.applicaiton.Features.CQRS.Handlers.AddressHandlers;

public class GetAddressQueryHandler
{
    private readonly IRepository<Address> _addressRepository;

    public GetAddressQueryHandler(IRepository<Address> addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public async Task<List<GetAddressQueryResult>> Handle()
    {
        var values = await _addressRepository.GetAllAsync();
        return values.Select(x => new GetAddressQueryResult
        {
            AdressID = x.AddressID,
            AddressDetail = x.AdressDetails,
            UserID = x.UserID,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Number = x.Number,
            Email = x.Email
            

        }).ToList();
    }
}