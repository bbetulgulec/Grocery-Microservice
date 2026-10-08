using Microsoft.AspNetCore.Http;

namespace manav.DtoLayer.CatalogDtos.GalleryDtos;

public class CreateGalleryDto
{
    public string GallaryName { get; set; }
    public string GalleryUrl { get; set; }
    public IFormFile? ImageFile { get; set; }

}