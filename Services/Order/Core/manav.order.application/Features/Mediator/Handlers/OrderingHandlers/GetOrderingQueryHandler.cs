using manav.order.applicaiton.Features.Mediator.Queries.OrderingQueries;
using manav.order.applicaiton.Features.Mediator.Results.OrdiringResults;
using manav.order.applicaiton.Interfaces;
using manav.order.domain.Entities;
using MediatR;

namespace manav.order.applicaiton.Features.Mediator.Handlers.OrderingHandlers;

public class GetOrderingQueryHandler:IRequestHandler<GetOrderingQuery,List<GetOrdiringQueryResult>>
{
    private readonly IRepository<Ordering> _repository;

    public GetOrderingQueryHandler(IRepository<Ordering> repository)
    {
        this._repository = repository;
    }

    public async Task<List<GetOrdiringQueryResult>> Handle(GetOrderingQuery request, CancellationToken cancellationToken)
    {
        var values = await _repository.GetAllAsync();
        return values.Select(x=> new GetOrdiringQueryResult
        {
            OrderingID = x.OrderingID,
            OrderDate = x.OrderDate,
            TotalPrice = x.TotalPrice,
            UserID = x.UserID
        }).ToList();
    }
}