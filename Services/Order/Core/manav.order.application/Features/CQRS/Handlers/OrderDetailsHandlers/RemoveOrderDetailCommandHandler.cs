using manav.order.applicaiton.Features.CQRS.Commands.OrderDetailsCommands;
using manav.order.applicaiton.Interfaces;
using manav.order.domain.Entities;

namespace manav.order.applicaiton.Features.CQRS.Handlers.OrderDetailsHandlers;

public class RemoveOrderDetailCommandHandler
{
    private readonly IRepository<OrderDetail> _repository;

    public RemoveOrderDetailCommandHandler(IRepository<OrderDetail> repository)
    {
        _repository = repository;
    }

    public async Task Handle(RemoveOrderDetailsCommand removeOrderDetailCommand)
    {
        var value = await _repository.GetByIdAsync(removeOrderDetailCommand.Id);
        await _repository.DeleteAsync(value);
    }
}


