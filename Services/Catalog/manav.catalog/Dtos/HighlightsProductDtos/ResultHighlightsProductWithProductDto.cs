using manav.catalog.Dtos.ProductDtos;

namespace manav.catalog.Dtos.HighlightsProductDtos;

public class ResultHighlightsProductWithProductDto
{
    public string HighlightsProductsID { get; set; }
    public string HighlightsProductsName { get; set; }
    public ResultProductDto Product { get; set; }  
}