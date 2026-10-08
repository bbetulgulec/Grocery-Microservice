using manav.DtoLayer.CatalogDtos.ProductsDtos;

namespace manav.DtoLayer.CatalogDtos.HighlightsProductDtos;

public class CreateHighlightsProductDto
{ 
    public string HighlightsProductsName { get; set; }
    public UpdateProductDto Product { get; set; }
}