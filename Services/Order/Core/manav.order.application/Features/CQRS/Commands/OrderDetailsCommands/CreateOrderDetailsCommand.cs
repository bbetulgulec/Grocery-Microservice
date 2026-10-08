namespace manav.order.applicaiton.Features.CQRS.Commands.OrderDetailsCommands;

public class CreateOrderDetailsCommand
{
    public string ProductID { get; set; }
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public int ProductAmount { get; set; }
    public decimal ProductTotalPrice { get; set; }
    public int  OrderingID { get; set; }
}