using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.Areas.Admin.ViewComponents.AdminLayoutViewComponents;

public class _AdminLayoutFooterViewComponentPartial:ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
    
}