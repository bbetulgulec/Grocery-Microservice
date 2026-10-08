using manav.catalog.Dtos.ProductImageDtos;

namespace manav.catalog.Services.ProductImageServices;

public interface IProductImageServices
{
    public Task CreateProductImageAsync(CreateProductImageDto productDetail);
    public Task UpdateProductImageAsync(UpdateProductImageDto productDetail);
    public Task DeleteProductImageAsync(String id);
    public Task<GetByIdProductImageDto> GetByIdPsroductImageAsync(string id);
    public Task<List<ResultProductImageDto>> GetAllProductImagAsync();
}