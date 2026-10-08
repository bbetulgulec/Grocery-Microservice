using manav.webUI.Services.CatalogServices.AboutServices;
using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.ViewComponents.UILayoutViewComponents;

public class _FooterUILayoutComponentPartial:ViewComponent
{
    private readonly IAboutService _aboutService;
    public _FooterUILayoutComponentPartial( IAboutService aboutService)
    {
        _aboutService = aboutService;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var aboutValue = await _aboutService.GetAboutAsync();
        return View(aboutValue);
     
    }
    
}