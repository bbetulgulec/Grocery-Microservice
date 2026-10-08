using manav.DtoLayer.CatalogDtos.ProductsDtos;

namespace manav.webUI.Services.ProductServices;

public class ProductService:IProductService
{
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ResultProductDto>> GetAllProductAsync()
    {
        var responseMassage = await _httpClient.GetAsync("products");
        var values = await responseMassage.Content.ReadFromJsonAsync<List<ResultProductDto>>();
        return values;
    }

    public async Task<UpdateProductDto> GetByIdProductAsync(string id)
    {
        var responseMassage = await _httpClient.GetAsync("products/"+id);
        var values = await responseMassage.Content.ReadFromJsonAsync<UpdateProductDto>();
        return values;    
    }

    public async Task UpdateProductDto(UpdateProductDto updateProductDto)
    {
        await _httpClient.PutAsJsonAsync<UpdateProductDto>("products", updateProductDto);
    }

    public  async Task CreateProductDto(CreateProductDto createProductDto)
    {
        await  _httpClient.PostAsJsonAsync<CreateProductDto>("products", createProductDto);
    }

    public async Task DeleteProductAsync(string id)
    {
        await _httpClient.DeleteAsync($"products?id={id}");
        
    }

    public async Task<List<ResultProductWithCategoryDto>> GetProductsWithCategoryAsync()
    {
        var responseMassage = await _httpClient.GetAsync("products/productlistwithcategory");
        var values = await responseMassage.Content.ReadFromJsonAsync<List<ResultProductWithCategoryDto>>();
        return values;
    }

    public async Task<List<ResultProductWithCategoryDto>> GetProductsWithCategoryByCategoryIdAsync(string categoryId)
    {
        var response = await _httpClient.GetAsync(
            $"products/ProductListWithCategoryByCategoryId" +
            $"?categoryId={Uri.EscapeDataString(categoryId)}");
        var values = await response.Content
            .ReadFromJsonAsync<List<ResultProductWithCategoryDto>>();
        return values;
    }
}