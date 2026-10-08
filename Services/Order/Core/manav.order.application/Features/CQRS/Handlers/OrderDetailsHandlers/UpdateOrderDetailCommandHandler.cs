using manav.order.applicaiton.Features.CQRS.Commands.OrderDetailsCommands;
using manav.order.applicaiton.Interfaces;
using manav.order.domain.Entities;

namespace manav.order.applicaiton.Features.CQRS.Handlers.OrderDetailsHandlers;

public class UpdateOrderDetailCommandHandler
{
    private readonly IRepository<OrderDetail> _repository;

    public UpdateOrderDetailCommandHandler(IRepository<OrderDetail> repository)
    {

        _repository = repository;
    }

    public async Task Handle(UpdateOrderDetailsCommand updateOrderDetailCommand)
    {
        var value = await _repository.GetByIdAsync(updateOrderDetailCommand.OrderDetailID);
        value.ProductName = updateOrderDetailCommand.ProductName;
        value.ProductPrice = updateOrderDetailCommand.ProductPrice;
        value.ProductTotalPrice = updateOrderDetailCommand.ProductTotalPrice;
        value.OrderingID = updateOrderDetailCommand.OrderingID;
        value.ProductAmount = updateOrderDetailCommand.ProductAmount;
        await _repository.UpdateAsync(value);
    }
}