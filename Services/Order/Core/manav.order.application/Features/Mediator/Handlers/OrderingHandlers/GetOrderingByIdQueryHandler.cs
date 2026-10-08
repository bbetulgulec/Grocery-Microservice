

using manav.order.applicaiton.Features.Mediator.Queries.OrderingQueries;
using manav.order.applicaiton.Features.Mediator.Results.OrdiringResults;
using manav.order.applicaiton.Interfaces;
using manav.order.domain.Entities;
using MediatR;

namespace manav.order.applicaiton.Features.Mediator.Handlers.OrderingHandlers;

public class GetOrderingByIdQueryHandler:IRequestHandler<GetOrderingByIdQuery,GetOrdiringByIdQueryResult>
{
    private readonly IRepository<Ordering> _orderingRepository;

    public GetOrderingByIdQueryHandler(IRepository<Ordering> orderingRepository)
    {
        _orderingRepository = orderingRepository;
    }

    public async Task<GetOrdiringByIdQueryResult> Handle(GetOrderingByIdQuery request, CancellationToken cancellationToken)
    {
        var value = await _orderingRepository.GetByIdAsync(request.Id);
        return new GetOrdiringByIdQueryResult
        {
            OrderingID = value.OrderingID,
            OrderDate = value.OrderDate,
            UserID = value.UserID,
            TotalPrice = value.TotalPrice,
        };

    }
}