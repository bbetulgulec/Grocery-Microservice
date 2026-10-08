using manav.DtoLayer.OrderDtos.OrderingDtos;
using manav.webUI.Services.OrderSevices.AddressServices;
using manav.webUI.Services.OrderSevices.OrderingServices;
using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.Areas.Admin.Controllers;

[Area("Admin")]
[Route("Admin/Order")]
public class OrderController:Controller
{
    private readonly IAddressService _addressService;
    private readonly IOrderingService  _orderService;
    public OrderController(IAddressService addressService, IOrderingService orderService)
    {
        _addressService = addressService;
        _orderService = orderService;
    }
    [Route("Index")]
    public async Task< IActionResult> Index()
    {

        OrderAddressViewbagList();
        var values = await _addressService.GetAllAddressAsync();
        return View(values);
    }
    [Route("DeleteAddress/{id}")]
    public async Task<IActionResult> DeleteAddress(int id)
    {
        await  _addressService.DeleteAddressAsync(id);
        return RedirectToAction("Index","Order",new { area = "Admin" });
      
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
        ViewBag.titles = "Kullanıcı Adresleri İşlemleri";
        ViewBag.home = "Ana sayfa";
        ViewBag.table = "Kullanıcı adresleri";
        ViewBag.tableList = "Kullanıcı adresleri Listesi";
    }
}