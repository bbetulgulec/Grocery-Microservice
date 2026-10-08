using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.ViewComponents.UILayoutViewComponents;

public class _HeroUILayoutComponentPartial:ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
    
}