using manav.webUI.Services.CatalogServices.HighlightsProductServices;
using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.ViewComponents.UILayoutViewComponents;

public class _SideListMenuUILayoutComponentPartial:ViewComponent
{
    private readonly IHighlightsProductService _highlightsProductService;
    public _SideListMenuUILayoutComponentPartial( IHighlightsProductService highlightsProductService)
    {
        _highlightsProductService = highlightsProductService;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var values = await _highlightsProductService.GetHighlightsProductListWithProductAsync();
        return View(values);
      
    }
    
}