using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.ViewComponents.UILayoutViewComponents;

public class _AutoPlayUILayout:ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
    
}