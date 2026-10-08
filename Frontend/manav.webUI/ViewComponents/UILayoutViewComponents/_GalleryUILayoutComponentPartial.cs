using manav.webUI.Services.CatalogServices.GalleryServices;
using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.ViewComponents.UILayoutViewComponents;

public class _GalleryUILayoutComponentPartial:ViewComponent
{
    private readonly IGalleryService _galleryService;
    public _GalleryUILayoutComponentPartial( IGalleryService galleryService)
    {
        _galleryService = galleryService;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var values = await _galleryService.GetAllGalleryAsync();
        return View(values);
    }
    
}