using manav.order.applicaiton.Features.CQRS.Queries.AddressQueries;
using manav.order.applicaiton.Features.CQRS.Result.AddressResult;
using manav.order.applicaiton.Interfaces;
using manav.order.domain.Entities;

namespace manav.order.applicaiton.Features.CQRS.Handlers.AddressHandlers;

public class GetAddressByIdQueryHandler
{
    private readonly IRepository<Address> _repository;

    public GetAddressByIdQueryHandler(IRepository<Address> repository)
    {
        _repository = repository;
    }

    public async Task<GetAddressByIdQueryResult> Handle(GetAddressByIdQuery request)
    {
        var values = await _repository.GetByIdAsync(request.Id);
        return new GetAddressByIdQueryResult
        {
            AdressID = values.AddressID,
            AddressDetail = values.AdressDetails,
            UserID = values.UserID,
        };
    }
}