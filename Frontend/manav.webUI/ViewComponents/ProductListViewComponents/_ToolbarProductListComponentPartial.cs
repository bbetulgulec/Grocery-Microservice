using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.ViewComponents.ProductListViewComponents;

public class _ToolbarProductListComponentPartial:ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
    
}