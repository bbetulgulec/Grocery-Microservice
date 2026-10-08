using manav.DtoLayer.CatalogDtos.ProductsDtos;

namespace manav.webUI.Services;

public interface IProductService
{
    Task<List<ResultProductDto>> GetAllProductAsync();
    Task<UpdateProductDto> GetByIdProductAsync(string id);
    Task UpdateProductDto(UpdateProductDto updateProductDto);
    Task CreateProductDto(CreateProductDto createProductDto);
    Task DeleteProductAsync(string id);
    Task<List<ResultProductWithCategoryDto>> GetProductsWithCategoryAsync();
    Task<List<ResultProductWithCategoryDto>> GetProductsWithCategoryByCategoryIdAsync(string categoryId);
}