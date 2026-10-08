using manav.catalog.Dtos.ProductDtos;

namespace manav.catalog.Dtos.HighlightsProductDtos;

public class CreateHighlightsProductDto
{

    public string HighlightsProductsName { get; set; }
    public UpdateProductDto Product { get; set; }
}