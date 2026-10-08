using manav.catalog.Dtos.GalleryDtos;
using manav.catalog.Services.GalleryServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace manav.catalog.Contollers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class GalleryController:Controller
{
    private readonly IGalleryServices _galleryServices;

    public GalleryController(IGalleryServices galleryServices)
    {
        _galleryServices = galleryServices;
    }

    [HttpGet]
    public async Task<IActionResult> GalleryList()
    {
        var values = await _galleryServices.GetAllGalleryAsync();
        return Ok(values);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGalleryById(string id)
    {
        var value= await _galleryServices.GetByIdGalleryAsync(id);
        return Ok(value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateGallery(CreateGalleryDto createGalleryDto)
    {
        await _galleryServices.CreateGalleryAsync(createGalleryDto);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteGallery(string id)
    {
        await _galleryServices.DeleteGalleryAsync(id);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateGallery(UpdateGalleryDto updateGalleryDto)
    {
        await _galleryServices.UpdateGalleryAsync(updateGalleryDto);
        return Ok();
    }
    
}