using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.Areas.Admin.Controllers;

[Area("Admin")]
[Route("Admin/About")]
public class AdminLayout : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}