namespace manav.DtoLayer.OrderDtos.OrderingDtos;

public class UserOrderSummaryDto
{
    public string UserID { get; set; }
    public int TotalOrderCount { get; set; }
    public DateTime LastOrderDate { get; set; }
}