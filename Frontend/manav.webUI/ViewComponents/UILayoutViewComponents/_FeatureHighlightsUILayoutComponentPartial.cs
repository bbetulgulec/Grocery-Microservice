using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.ViewComponents.UILayoutViewComponents;

public class _FeatureHighlightsUILayoutComponentPartial:ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
    
}