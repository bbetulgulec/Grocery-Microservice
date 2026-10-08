using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.ViewComponents.UILayoutViewComponents;

public class _FeedbackUILayoutComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        ViewBag.AboutNumber = "+90 554 004 24 01";
        return View();
    }
}