using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.ViewComponents.ProductListViewComponents;

public class _FilterProductListComponentPartial:ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
    
}