namespace manav.order.applicaiton.Features.CQRS.Queries.OrderDetailQueries;

public class GetOrderDetailByIdQuery
{
    public int  Id{ get; set; }

    public GetOrderDetailByIdQuery(int id)
    {
        Id = id;
    }
}