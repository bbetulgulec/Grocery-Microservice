using manav.webUI.Services.BasketServices;
using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.ViewComponents.UILayoutViewComponents;

public class _OrderProductListUILayoutComponentPartial:ViewComponent
{
    private readonly IBasketService _basketService;

    public _OrderProductListUILayoutComponentPartial(IBasketService basketService)
    {
        _basketService = basketService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var basketValues = await _basketService.GetBasket();
        return View(basketValues);
    }
    
}