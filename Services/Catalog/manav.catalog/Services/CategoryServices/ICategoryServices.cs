using manav.catalog.Dtos.CategoryDtos;

namespace manav.catalog.Services.CategoryServices;

public interface ICategoryServices
{
   
    Task<List<ResultCategoryDto>> GetAllCategoriesAsync();
    Task CreateCategoryAsync(CreateCategoryDto CreateCategoryDto);
    Task UpdateCategoryAsync(UpdateCategoryDto UpdateCategoryDto);
    Task DeleteCategoryAsync(string id);
    Task<GetByIDcategoryDto> GetByIdCategoryAsync(string id);
}