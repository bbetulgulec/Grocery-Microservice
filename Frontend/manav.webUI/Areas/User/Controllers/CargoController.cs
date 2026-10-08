using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.Areas.User.Controllers;

[Area("User")]
[Route("User/Kargo")]
public class CargoController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}