using manav.DtoLayer.CatalogDtos.GalleryDtos;

namespace manav.webUI.Services.CatalogServices.GalleryServices;

public interface IGalleryService
{
    Task<List<ResultGalleryDto>> GetAllGalleryAsync();
    Task CreateGalleryAsync(CreateGalleryDto createGalleryDto);
    Task UpdateGalleryAsync(UpdateGalleryDto updateGalleryDto);
    Task DeleteGalleryAsync(string id);
    Task<UpdateGalleryDto> GetByIdGalleryAsync(string id);
}