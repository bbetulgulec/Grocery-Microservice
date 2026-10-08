using manav.order.applicaiton.Features.Mediator.Commands.OrderingCommands;
using manav.order.applicaiton.Interfaces;
using manav.order.domain.Entities;
using MediatR;

namespace manav.order.applicaiton.Features.Mediator.Handlers.OrderingHandlers;

public class CreateOrderingCommandHandler:IRequestHandler<CreateOrderingCommand,int>
{
    private readonly IRepository<Ordering> _repository;

    public CreateOrderingCommandHandler(IRepository<Ordering> repository)
    {
        _repository = repository;
    }


    public async Task<int> Handle(CreateOrderingCommand request, CancellationToken cancellationToken)
    {
        var ordering = new Ordering
        {
            OrderDate = request.OrderDate,
            TotalPrice = request.TotalPrice,
            UserID = request.UserID
        };

        await _repository.CreateAsync(ordering);
        return ordering.OrderingID;
    }
}