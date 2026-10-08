using manav.DtoLayer.OrderDtos.AddressDtos;
using manav.DtoLayer.OrderDtos.OrderingDtos;
using manav.webUI.Services.BasketServices;
using manav.webUI.Services.Interfaces;
using manav.webUI.Services.OrderSevices.AddressServices;
using manav.webUI.Services.OrderSevices.OrderingServices;
using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.Controllers;

public class OrderController : Controller
{
    private readonly IAddressService _addressService;
    private readonly IBasketService _basketService;
    private readonly IOrderingService _orderingService;
    private readonly IUserService _userService;

    public OrderController(
        IAddressService addressService,
        IUserService userService,
        IOrderingService orderingService,
        IBasketService basketService)
    {
        _addressService = addressService;
        _userService = userService;
        _orderingService = orderingService;
        _basketService = basketService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateAddress(CreateAddressDto dto)
    {
        var user = await _userService.GetUser();
        dto.UserID = user.Id;
        await _addressService.CreateAddressAsync(dto);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder()
    {
        var user = await _userService.GetUser();
        var basket = await _basketService.GetBasket();
        if (!basket.BasketItems.Any()) return RedirectToAction("Index", "Basket");
        var createOrderingDto = new CreateOrderingDto
        {
            UserID = user.Id,
            TotalPrice = basket.TotalPrice + (basket.TotalPrice > 0 ? 29.90m : 0m),
            OrderDate = DateTime.Now
        };
        var orderingId = await _orderingService.CreateOrderingAsync(createOrderingDto);
        if (orderingId > 0)
        {
            foreach (var item in basket.BasketItems)
            {
                var createOrderDetailDto = new CreateOrderDetailDto
                {
                    OrderingID = orderingId,
                    ProductID = item.ProductId,
                    ProductName = item.ProductName,
                    ProductPrice = item.Price,
                    ProductAmount = item.Quantity,
                    ProductTotalPrice = item.Price * item.Quantity
                };

                await _orderingService.CreateOrderDetailAsync(createOrderDetailDto);
            }

            await _basketService.DeleteBasket(user.Id);
            return RedirectToAction("EmployeeReferral", "Basket");
        }

        return RedirectToAction("Index", "Basket");
    }

    [HttpGet]
    public async Task<IActionResult> UserOrderList()
    {
        var user = await _userService.GetUser();

        if (user == null) return RedirectToAction("Index", "Login");

        var orders = await _orderingService.GetOrderingByUserId(user.Id);

        return View(orders);
    }
}