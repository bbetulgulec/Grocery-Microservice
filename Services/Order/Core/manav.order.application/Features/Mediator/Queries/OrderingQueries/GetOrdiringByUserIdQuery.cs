using manav.order.applicaiton.Features.Mediator.Results.OrdiringResults;
using MediatR;

namespace manav.order.applicaiton.Features.Mediator.Queries.OrderingQueries;

public class GetOrdiringByUserIdQuery:IRequest<List<GetOrdiringByUserIdQueryResult>>
{
    public string Id { get; set; }

    public GetOrdiringByUserIdQuery(string id)
    {
        Id = id;
    }
}