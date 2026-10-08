namespace manav.DtoLayer.OrderDtos.OrderingDtos;

public class CreateOrderingResponseDto
{
    public int OrderingID { get; set; }
    public string UserID { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime OrderDate { get; set; }
}