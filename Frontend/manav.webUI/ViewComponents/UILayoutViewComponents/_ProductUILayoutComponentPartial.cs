using manav.webUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.ViewComponents.UILayoutViewComponents;

public class _ProductUILayoutComponentPartial : ViewComponent
{
    private readonly IProductService _productService;
    public _ProductUILayoutComponentPartial( IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var values = await _productService.GetAllProductAsync();
        var random = values.OrderBy(x => Guid.NewGuid()).Take(4).ToList();
        return View(random);
    }
}