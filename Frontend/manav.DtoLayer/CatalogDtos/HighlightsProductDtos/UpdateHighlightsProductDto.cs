using manav.DtoLayer.CatalogDtos.ProductsDtos;

namespace manav.DtoLayer.CatalogDtos.HighlightsProductDtos;

public class UpdateHighlightsProductDto
{
    public string HighlightsProductsID { get; set; }
    public string HighlightsProductsName { get; set; }
    public UpdateProductDto Product { get; set; }
}