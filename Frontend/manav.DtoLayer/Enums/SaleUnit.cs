using System.Text.Json.Serialization;

namespace manav.DtoLayer.CatalogDtos.ProductsDtos;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SaleUnit
{
    Piece,
    Kg,
    Pack
}