using manav.order.applicaiton.Features.Mediator.Results.OrdiringResults;
using MediatR;

namespace manav.order.applicaiton.Features.Mediator.Queries.OrderingQueries;

public class GetOrderingByIdQuery:IRequest<GetOrdiringByIdQueryResult>
{
    public int Id { get; set; }

    public GetOrderingByIdQuery(int id)
    {
        Id = id;
    }
}