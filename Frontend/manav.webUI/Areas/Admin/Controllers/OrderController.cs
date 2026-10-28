using manav.DtoLayer.OrderDtos.OrderingDtos;
using manav.webUI.Services.OrderSevices.AddressServices;
using manav.webUI.Services.OrderSevices.OrderingServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace manav.webUI.Areas.Admin.Controllers;

[Area("Admin")]
[Route("Admin/Order")]
public class OrderController : Controller
{
    private readonly IAddressService _addressService;
    private readonly IStringLocalizer<SharedResource> _l;
    private readonly IOrderingService _orderService;

    public OrderController(IAddressService addressService, IOrderingService orderService,
        IStringLocalizer<SharedResource> l)
    {
        _addressService = addressService;
        _orderService = orderService;
        _l = l;
    }

    [Route("Index")]
    public async Task<IActionResult> Index()
    {
        OrderAddressViewbagList();
        var values = await _addressService.GetAllAddressAsync();
        return View(values);
    }

    [Route("DeleteAddress/{id}")]
    public async Task<IActionResult> DeleteAddress(int id)
    {
        await _addressService.DeleteAddressAsync(id);
        return RedirectToAction("Index", "Order", new { area = "Admin" });
    }

    [Route("GetAllOrders")]
    public async Task<IActionResult> GetAllOrders()
    {
        OrderAddressViewbagList();
        var orders = await _orderService.GetAllOrdersAsync();
        var userOrderSummary = orders
            .GroupBy(x => x.UserID)
            .Select(g => new UserOrderSummaryDto
            {
                UserID = g.Key,
                TotalOrderCount = g.Count(),
                LastOrderDate = g.Max(x => x.OrderDate)
            })
            .OrderByDescending(x => x.LastOrderDate)
            .ToList();

        return View(userOrderSummary);
    }

    [Route("OrderListByUserId/{userId}")]
    public async Task<IActionResult> OrderListByUserId(string userId)
    {
        OrderAddressViewbagList();
        var userOrders = await _orderService.GetOrderingByUserId(userId);
        var sortedOrders = userOrders
            .OrderByDescending(x => x.OrderDate)
            .ToList();

        return View(sortedOrders);
    }

    public void OrderAddressViewbagList()
    {
        ViewBag.titles = _l["Admin_UserAddress_Title"].Value;
        ViewBag.home = _l["Common_Home"].Value;
        ViewBag.table = _l["Admin_UserAddress_Table"].Value;
        ViewBag.tableList = _l["Admin_UserAddress_TableList"].Value;
    }
}