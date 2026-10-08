using Microsoft.AspNetCore.Http;

namespace manav.DtoLayer.CatalogDtos.GalleryDtos;

public class UpdateGalleryDto
{
    public string GallaryId { get; set; }
    public string GallaryName { get; set; }
    public string GalleryUrl { get; set; }
    public IFormFile? ImageFile { get; set; }
}