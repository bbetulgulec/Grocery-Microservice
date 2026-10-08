using ResultProductDto = manav.DtoLayer.CatalogDtos.ProductsDtos.ResultProductDto;

namespace manav.DtoLayer.CatalogDtos.HighlightsProductDtos;

public class ResultHighlightsProductWithProductDto
{
    public string HighlightsProductsID { get; set; }
    public string HighlightsProductsName { get; set; }
    public ResultProductDto Product { get; set; }
}