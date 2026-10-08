using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.Areas.User.Controllers;

[Area("User")]
[Route("User/Logout")]
public class LogoutController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}