using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.Controllers;

public class PaymentController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult FailedPayment()
    {
        return View();
    }
}