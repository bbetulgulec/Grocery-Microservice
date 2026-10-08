using manav.DtoLayer.CatalogDtos.CategoryDtos;

namespace manav.webUI.Services.CatalogServices.CategoryServices;

public interface ICategoryService
{
    Task<List<ResultCategoryDtos>> GetAllCategoriesAsync();
    Task CreateCategoryAsync(CreateCategoryDtos createCategoryDto);
    Task UpdateCategoryAsync(UpdateCategoryDtos updateCategoryDto);
    Task DeleteCategoryAsync(string id);
    Task<UpdateCategoryDtos> GetByIdCategoryAsync(string id);
}