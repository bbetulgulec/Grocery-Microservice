using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.Controllers;

public class DefaultController:Controller
{
    public IActionResult Index()
    {
        return View();
    }
    
} 