using manav.order.applicaiton.Features.Mediator.Commands.OrderingCommands;
using manav.order.applicaiton.Interfaces;
using manav.order.domain.Entities;
using MediatR;

namespace manav.order.applicaiton.Features.Mediator.Handlers.OrderingHandlers;

public class RemoveOrderingCommandHandler:IRequestHandler<RemoveOrderingCommand>
{
    private readonly IRepository<Ordering> _repository;

    public RemoveOrderingCommandHandler(IRepository<Ordering> repository)
    {
        _repository = repository;
    }

    public async Task Handle(RemoveOrderingCommand request, CancellationToken cancellationToken)
    {
        var value= await _repository.GetByIdAsync(request.Id);
        await _repository.DeleteAsync(value);
    }
}