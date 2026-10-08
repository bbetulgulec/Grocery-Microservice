using manav.order.applicaiton.Features.Mediator.Queries.OrderingQueries;
using manav.order.applicaiton.Features.Mediator.Results.OrdiringResults;
using manav.order.applicaiton.Interfaces;
using manav.order.domain.Entities;
using MediatR;

namespace manav.order.applicaiton.Features.Mediator.Handlers.OrderingHandlers;

public class GetOrdiringByUserIdQueryHandler:IRequestHandler<GetOrdiringByUserIdQuery,List<GetOrdiringByUserIdQueryResult>>
{
    private readonly IOrderingRepository _repository;

    public GetOrdiringByUserIdQueryHandler(IOrderingRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<GetOrdiringByUserIdQueryResult>> Handle(GetOrdiringByUserIdQuery request, CancellationToken cancellationToken)
    {
     var values =  _repository.GetOrderingsByUserId(request.Id);
     return values.Select(x => new GetOrdiringByUserIdQueryResult
     {
         OrderDate = x.OrderDate,
         OrderingID = x.OrderingID,
         TotalPrice = x.TotalPrice,
         UserID = x.UserID
     }).ToList();

    }
}
