using manav.DtoLayer.CatalogDtos.HighlightsProductDtos;

namespace manav.webUI.Services.CatalogServices.HighlightsProductServices;

public interface IHighlightsProductService
{
    Task<List<ResultHighlightsProductDto>> GetAllHighlightsProductAsync();
    Task CreateHighlightsProductAsync(CreateHighlightsProductDto createHighlightsProductDto);
    Task UpdateHighlightsProductAsync(UpdateHighlightsProductDto updateHighlightsProductDto);
    Task DeleteHighlightsProductAsync(string id);
    Task<UpdateHighlightsProductDto> GetByIdHighlightsProductAsync(string id);
    Task<List<ResultHighlightsProductWithProductDto>> GetHighlightsProductListWithProductAsync();
}