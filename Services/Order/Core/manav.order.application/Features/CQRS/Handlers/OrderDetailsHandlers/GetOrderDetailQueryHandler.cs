using manav.order.applicaiton.Features.CQRS.Queries.OrderDetailQueries;
using manav.order.applicaiton.Features.Mediator;
using manav.order.applicaiton.Interfaces;
using manav.order.domain.Entities;

namespace manav.order.applicaiton.Features.CQRS.Handlers.OrderDetailsHandlers;

public class GetOrderDetailQueryHandler
{
    private readonly IRepository<OrderDetail> _repository;

    public GetOrderDetailQueryHandler(IRepository<OrderDetail> repository)
    {
        _repository = repository;
    }

    public async Task<List<GetOrderDetailQueryResult>> Handle()
    {
        var values = await _repository.GetAllAsync();
        return values.Select(x => new GetOrderDetailQueryResult
        {
           ProductName = x.ProductName,
           ProductAmount = x.ProductAmount,
           ProductTotalPrice = x.ProductTotalPrice,
           OrderingID = x.OrderingID,
           OrderDetailID =  x.OrderDetailID, 
           ProductID =  x.ProductID,
           ProductPrice =   x.ProductPrice,
        }).ToList();
    }
}