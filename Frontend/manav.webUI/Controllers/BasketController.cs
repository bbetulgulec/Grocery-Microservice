using manav.DtoLayer.BasketDtos;
using manav.webUI.Enums;
using manav.webUI.Services;
using manav.webUI.Services.BasketServices;
using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.Controllers;

public class BasketController : Controller
{
    private readonly IBasketService _basketService;
    private readonly IProductService _productService;

    public BasketController(IBasketService basketService, IProductService productService)
    {
        _basketService = basketService;
        _productService = productService;
    }

    public async Task<IActionResult> Index()
    {
        var basket = await _basketService.GetBasket();
        return View(basket);
    }

    public async Task<IActionResult> AddToBasket(string productId)
    {
        if (User.Identity?.IsAuthenticated != true) return RedirectToAction("Index", "Login");
        var basket = await _productService.GetByIdProductAsync(productId);
        var item = new BasketItemDto
        {
            ProductId = basket.ProductID,
            ProductName = basket.ProductName,
            Price = basket.ProductPrice,
            Quantity = 1
        };
        await _basketService.AddItemToBasket(item);
        return RedirectToAction("Index", "Basket");
    }

    public async Task<IActionResult> RemoveFromBasket(string productId)
    {
        await _basketService.RemoveItemFromBasket(productId);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> ChangeCount(string productId, int changeBy)
    {
        await _basketService.ChangeTheCountOfProduct(productId, changeBy);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> ContinueToPayment(PaymentMethod paymentMethod)
    {
        switch (paymentMethod)
        {
            case PaymentMethod.Cash:
                return RedirectToAction("Index", "Order");
            case PaymentMethod.Card:
                return RedirectToAction("Index", "Payment");
            case PaymentMethod.Pickup:
                return RedirectToAction("EmployeeReferral", "Basket");
            default:
                return RedirectToAction("Index", "Basket");
        }
    }

    public IActionResult EmployeeReferral()
    {
        return View();
    }
}