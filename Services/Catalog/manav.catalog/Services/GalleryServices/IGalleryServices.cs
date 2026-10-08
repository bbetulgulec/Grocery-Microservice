using manav.catalog.Dtos.GalleryDtos;

namespace manav.catalog.Services.GalleryServices;

public interface IGalleryServices
{
    Task<List<ResultGalleryDto>> GetAllGalleryAsync();
    Task CreateGalleryAsync(CreateGalleryDto createGalleryDto);
    Task UpdateGalleryAsync(UpdateGalleryDto updateGalleryDto);
    Task DeleteGalleryAsync(string id);
    Task<GetByIdGalleryDto> GetByIdGalleryAsync(string id);
}