using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.Areas.User.Controllers;

[Area("User")]
[Route("User/Profile")]
public class ProfileController:Controller
{
    public IActionResult Index()
    {
        return View();
    }
    
}