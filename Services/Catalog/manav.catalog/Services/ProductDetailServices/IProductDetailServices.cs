using manav.catalog.Dtos.ProductDetailsDtos;

namespace manav.catalog.Services.ProductDetailServices;

public interface IProductDetailServices
{
    public Task CreateProductDetailAsync(CreateProductDetailsDto productDetail);
    public Task UpdateProductDetailAsync(UpdateProductDetailDto productDetail);
    public Task DeleteProductDetailAsync(String id);
    public Task<GetByIdProductDetailDto> GetByIdProductDetailAsync(string id);
    public Task<List<ResultProductDetailDto>> GetAllProductDetailAsync();
}