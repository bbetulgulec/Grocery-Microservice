namespace manav.order.applicaiton.Features.Mediator.Results.OrdiringResults;

public class GetOrdiringQueryResult
{
    public int OrderingID { get; set; }
    public string UserID { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime OrderDate { get; set; }
}