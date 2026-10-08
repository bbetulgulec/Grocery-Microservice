namespace manav.DtoLayer.OrderDtos.OrderingDtos;

public class CreateOrderingDto
{
    public string UserID { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime OrderDate { get; set; }
}