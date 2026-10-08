using manav.order.applicaiton.Features.Mediator.Commands.OrderingCommands;
using manav.order.applicaiton.Interfaces;
using manav.order.domain.Entities;
using MediatR;

namespace manav.order.applicaiton.Features.Mediator.Handlers.OrderingHandlers;

public class UpdateOrderingCommandHandler:IRequestHandler<UpdateOrderingCommand>
{
    private readonly IRepository<Ordering> _repository;

    public UpdateOrderingCommandHandler(IRepository<Ordering> repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateOrderingCommand request, CancellationToken cancellationToken)
    {
        var value = await _repository.GetByIdAsync(request.OrderingID);
        value.OrderDate = request.OrderDate;
        value.UserID = request.UserID;
        value.TotalPrice = request.TotalPrice;
        await _repository.UpdateAsync(value);
        
    }
}