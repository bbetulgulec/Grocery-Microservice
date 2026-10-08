using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.Areas.User.ViewComponents.UserLayoutViewComponents;

public class _UserLayoutHeadViewComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}