using manav.catalog.Dtos.HighlightsProductDtos;

namespace manav.catalog.Services.HighlightsProductsServices;

public interface IHighlightsProductServices
{
    
   
    Task<List<ResultHighlightsProductDto>> GetAllHighlightsProductAsync();
    Task CreateHighlightsProductAsync(CreateHighlightsProductDto createHighlightsProductDto);
    Task UpdateHighlightsProductAsync(UpdateHighlightsProductDto updateHighlightsProductDto);
    Task DeleteHighlightsProductAsync(string id);
    Task<GetByIDHighlightsProductDto> GetByIdHighlightsProductAsync(string id);
    Task<List<ResultHighlightsProductWithProductDto>> GetHighlightsProductListWithProductAsync();

}