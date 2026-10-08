using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.Controllers;

public class UILayoutController : Controller
{
    public IActionResult _UILayout()
    {
        return View();
    }
}