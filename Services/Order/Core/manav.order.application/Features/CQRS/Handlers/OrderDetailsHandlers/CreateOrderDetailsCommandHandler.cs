using manav.order.applicaiton.Features.CQRS.Commands.OrderDetailsCommands;
using manav.order.applicaiton.Interfaces;
using manav.order.domain.Entities;

namespace manav.order.applicaiton.Features.CQRS.Handlers.OrderDetailsHandlers;

public class CreateOrderDetailsCommandHandler
{
    private readonly IRepository<OrderDetail> _repository;
    public CreateOrderDetailsCommandHandler(IRepository<OrderDetail> repository)
    {
        _repository = repository;
    }

    public async Task Handle(CreateOrderDetailsCommand request)
    {
        await _repository.CreateAsync(new OrderDetail
        {
            ProductName =  request.ProductName,
            ProductAmount = request.ProductAmount,
            ProductTotalPrice = request.ProductTotalPrice,
            ProductPrice =  request.ProductPrice,
            OrderingID = request.OrderingID,
            ProductID =  request.ProductID
        });

    }
}