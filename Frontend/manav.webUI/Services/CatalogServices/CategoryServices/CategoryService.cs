using manav.DtoLayer.CatalogDtos.CategoryDtos;

namespace manav.webUI.Services.CatalogServices.CategoryServices;

public class CategoryService:ICategoryService
{
    private readonly HttpClient _httpClient;

    public CategoryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ResultCategoryDtos>> GetAllCategoriesAsync()
    {
        var responseMassage = await _httpClient.GetAsync("categories");
        var values = await responseMassage.Content.ReadFromJsonAsync<List<ResultCategoryDtos>>();
        return values;
    }

    public async Task CreateCategoryAsync(CreateCategoryDtos createCategoryDto)
    {
         await  _httpClient.PostAsJsonAsync<CreateCategoryDtos>("categories", createCategoryDto);
    }

    public async Task UpdateCategoryAsync(UpdateCategoryDtos updateCategoryDto)
    {
        await _httpClient.PutAsJsonAsync<UpdateCategoryDtos>("categories", updateCategoryDto);
        
    }

    public async Task DeleteCategoryAsync(string id)
    {
        await _httpClient.DeleteAsync($"categories?id={id}");
    }

    public async Task<UpdateCategoryDtos> GetByIdCategoryAsync(string id)
    {
        var responseMassage = await _httpClient.GetAsync("categories/"+id);
        var values = await responseMassage.Content.ReadFromJsonAsync<UpdateCategoryDtos>();
        return values;

    }
}