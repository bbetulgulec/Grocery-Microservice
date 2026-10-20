using manav.DtoLayer.CatalogDtos.ProductsDtos;
using manav.webUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.Controllers;

public class ProductListController : Controller
{
    private readonly IProductService _productService;

    public ProductListController(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IActionResult> Index()
    {
        var initialProducts = await _productService.GetProductsWithCategoryAsync();
        return View(initialProducts);
    }

    [HttpGet]
    public async Task<IActionResult> GetProductsByCategory(
        [FromQuery] List<string> categoryIds)
    {
        var allProducts = new List<ResultProductWithCategoryDto>();

        if (categoryIds.Count == 0)
            allProducts = await _productService.GetProductsWithCategoryAsync();
        else
            foreach (var id in categoryIds)
            {
                var products =
                    await _productService.GetProductsWithCategoryByCategoryIdAsync(id);

                if (products != null)
                    allProducts.AddRange(products);
            }

        return PartialView(
            "~/Views/Shared/Components/_ProductsProductListComponentPartial/Default.cshtml",
            allProducts);
    }
}