using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.Controllers;

public class ErrorController:Controller
{

    public IActionResult Index()
    {
        return View();
    }
    
}