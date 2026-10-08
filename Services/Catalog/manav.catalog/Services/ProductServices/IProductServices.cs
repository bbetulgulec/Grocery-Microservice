using manav.catalog.Dtos.ProductDtos;

namespace manav.catalog.Services.ProductServices;

public interface IProductServices
{
    Task<List<ResultProductDto>> GetAllCategoriesAsync();
    Task<UpdateProductDto> GetByIdProductAsync(string id);
    Task UpdateProductDto(UpdateProductDto updateProductDto);
    Task CreateProductDto(CreateProductDto createProductDto);
    Task DeleteProductAsync(string id);
    Task<List<ResultProductWithCategoryDto>> GetProductsWithCategoryAsync();
    Task<List<ResultProductWithCategoryDto>> GetProductsWithCategoryByCategoryIdAsync(string categoryId);
}