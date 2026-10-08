namespace manav.order.applicaiton.Features.Mediator.Results.OrdiringResults;

public class GetOrdiringByIdQueryResult
{
    public int OrderingID { get; set; }
    public string UserID { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime OrderDate { get; set; }
}