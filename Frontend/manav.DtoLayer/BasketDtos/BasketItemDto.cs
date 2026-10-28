using System.Text.Json.Serialization;
using manav.DtoLayer.CatalogDtos.ProductsDtos;

namespace manav.DtoLayer.BasketDtos;

public class BasketItemDto
{
    [JsonPropertyName("productId")] public string ProductId { get; set; }

    [JsonPropertyName("productName")] public string ProductName { get; set; }

    [JsonPropertyName("price")] public decimal Price { get; set; }

    [JsonPropertyName("quantity")] public int Quantity { get; set; }

    [JsonPropertyName("unit")] public SaleUnit Unit { get; set; }
}