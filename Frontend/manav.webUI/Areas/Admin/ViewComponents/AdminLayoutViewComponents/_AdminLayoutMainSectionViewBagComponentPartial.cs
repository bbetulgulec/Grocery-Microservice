using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.Areas.Admin.ViewComponents.AdminLayoutViewComponents;

public class _AdminLayoutMainSectionViewBagComponentPartial:ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
    
}