using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.ViewComponents.UILayoutViewComponents;

public class _MenuUILayoutComponentPartial:ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}