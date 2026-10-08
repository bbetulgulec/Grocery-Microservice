using manav.DtoLayer.OrderDtos.OrderingDtos;
using manav.webUI.Services.Interfaces;
using manav.webUI.Services.OrderSevices.OrderingServices;
using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.Areas.User.Controllers;

[Area("User")]
[Route("User/MyOrder/[action]")]
public class MyOrderController : Controller
{
    private readonly IOrderingService _orderingService;
    private readonly IUserService _userService;

    public MyOrderController(IOrderingService orderingService, IUserService userService)
    {
        _orderingService = orderingService;
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> MyOrderList()
    {
        var user = await _userService.GetUser();
        var values = await _orderingService.GetOrderingByUserId(user.Id);
        return View(values);
    }
}