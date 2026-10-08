using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.ViewComponents.ProductListViewComponents;

public class _PaginationProductListComponentPartial:ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}