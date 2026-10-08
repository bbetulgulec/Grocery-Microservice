using manav.webUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.Controllers;

public class ProductDetailController : Controller
{
    private readonly IProductService _productService;

    public ProductDetailController(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IActionResult> Index(string id)
    {
        var value = await _productService.GetByIdProductAsync(id);
        return View(value);
    }
}