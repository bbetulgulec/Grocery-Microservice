using manav.webUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.ViewComponents.ProductListViewComponents;

public class _ProductsProductListComponentPartial:ViewComponent
{
    
    private readonly IProductService _productService;
    public _ProductsProductListComponentPartial( IProductService productService)
    {
        _productService = productService;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var values = await _productService.GetProductsWithCategoryAsync();
        return View(values);
    }
}