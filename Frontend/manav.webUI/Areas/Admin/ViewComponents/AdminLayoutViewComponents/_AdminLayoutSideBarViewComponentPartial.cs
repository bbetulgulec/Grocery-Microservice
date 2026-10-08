using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.Areas.Admin.ViewComponents.AdminLayoutViewComponents;

public class _AdminLayoutSideBarViewComponentPartial:ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
    
}