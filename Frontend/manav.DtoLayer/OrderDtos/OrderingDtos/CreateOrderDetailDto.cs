namespace manav.DtoLayer.OrderDtos.OrderingDtos;

public class CreateOrderDetailDto
{
    public string ProductID { get; set; }
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public int ProductAmount { get; set; }
    public decimal ProductTotalPrice { get; set; }
    public int OrderingID { get; set; }
}