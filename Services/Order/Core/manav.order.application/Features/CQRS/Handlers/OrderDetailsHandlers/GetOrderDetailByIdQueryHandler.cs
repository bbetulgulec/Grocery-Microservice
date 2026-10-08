using manav.order.applicaiton.Features.CQRS.Queries.OrderDetailQueries;
using manav.order.applicaiton.Features.CQRS.Result.AddressResult;
using manav.order.applicaiton.Features.Mediator;
using manav.order.applicaiton.Interfaces;
using manav.order.domain.Entities;

namespace manav.order.applicaiton.Features.CQRS.Handlers.OrderDetailsHandlers;

public class GetOrderDetailByIdQueryHandler
{
    
    private readonly IRepository<OrderDetail>  _orderDetailRepository;

    public GetOrderDetailByIdQueryHandler(IRepository<OrderDetail> orderDetailRepository)
    {
        _orderDetailRepository = orderDetailRepository;
    }

    public async Task<GetOrderDetailByIdQueryResult>Handle(GetOrderDetailByIdQuery request)
    {
        var value= await _orderDetailRepository.GetByIdAsync(request.Id);
        return new GetOrderDetailByIdQueryResult
        {
            ProductName = value.ProductName,
            ProductPrice = value.ProductPrice,
            ProductAmount = value.ProductAmount,
            ProductID = value.ProductID,
            OrderingID = value.OrderingID,
            OrderDetailID = value.OrderDetailID,


        };
    }
}