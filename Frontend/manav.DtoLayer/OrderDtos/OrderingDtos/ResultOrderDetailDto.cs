using Newtonsoft.Json;

namespace manav.DtoLayer.OrderDtos.OrderingDtos;

public class ResultOrderDetailDto
{
    [JsonProperty("orderDetailID")]
    public int OrderDetailID { get; set; }

    [JsonProperty("productID")]
    public string ProductID { get; set; }

    [JsonProperty("productName")]
    public string ProductName { get; set; }

    [JsonProperty("productPrice")]
    public decimal ProductPrice { get; set; }

    [JsonProperty("productAmount")]
    public int ProductAmount { get; set; }

    [JsonProperty("productTotalPrice")]
    public decimal ProductTotalPrice { get; set; }

    [JsonProperty("orderingID")]
    public int OrderingID { get; set; }
}