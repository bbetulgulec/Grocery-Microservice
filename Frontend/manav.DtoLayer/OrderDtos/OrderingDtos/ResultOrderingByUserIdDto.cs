namespace manav.DtoLayer.OrderDtos.OrderingDtos;

public class ResultOrderingByUserIdDto
{
    public int OrderingID { get; set; }
    public string UserID { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime OrderDate { get; set; }
    public List<ResultOrderDetailDto> OrderDetails { get; set; }
}