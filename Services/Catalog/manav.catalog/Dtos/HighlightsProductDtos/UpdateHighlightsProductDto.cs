using manav.catalog.Dtos.ProductDtos;

namespace manav.catalog.Dtos.HighlightsProductDtos;

public class UpdateHighlightsProductDto
{
    public string HighlightsProductsID { get; set; }
    public string HighlightsProductsName { get; set; }
    public UpdateProductDto Product { get; set; }  
}