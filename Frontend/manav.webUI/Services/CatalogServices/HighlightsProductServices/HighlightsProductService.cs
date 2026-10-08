using manav.DtoLayer.CatalogDtos.HighlightsProductDtos;

namespace manav.webUI.Services.CatalogServices.HighlightsProductServices;

public class HighlightsProductService:IHighlightsProductService
{
    private readonly HttpClient _httpClient;

    public HighlightsProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<List<ResultHighlightsProductDto>> GetAllHighlightsProductAsync()
    {
        var responseMassage = await _httpClient.GetAsync("highlightsproduct");
        var values = await responseMassage.Content.ReadFromJsonAsync<List<ResultHighlightsProductDto>>();
        return values;
    }

    public async Task CreateHighlightsProductAsync(CreateHighlightsProductDto createHighlightsProductDto)
    {
        await  _httpClient.PostAsJsonAsync<CreateHighlightsProductDto>("highlightsproduct", createHighlightsProductDto);
    }

    public async Task UpdateHighlightsProductAsync(UpdateHighlightsProductDto updateHighlightsProductDto)
    {
        await _httpClient.PutAsJsonAsync<UpdateHighlightsProductDto>("highlightsproduct", updateHighlightsProductDto);
    }

    public async Task DeleteHighlightsProductAsync(string id)
    {
        await _httpClient.DeleteAsync($"highlightsproduct?id={id}");
    }

    public async Task<UpdateHighlightsProductDto> GetByIdHighlightsProductAsync(string id)
    {
        var responseMassage = await _httpClient.GetAsync("highlightsproduct/"+id);
        var values = await responseMassage.Content.ReadFromJsonAsync<UpdateHighlightsProductDto>();
        return values;    }

    public async Task<List<ResultHighlightsProductWithProductDto>> GetHighlightsProductListWithProductAsync()
    {
        var responseMassage = await _httpClient.GetAsync("highlightsproduct/highlightsproductlistwithproduct");
        var values = await responseMassage.Content.ReadFromJsonAsync<List<ResultHighlightsProductWithProductDto>>();
        return values;    }
}