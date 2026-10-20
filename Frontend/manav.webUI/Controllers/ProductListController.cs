using manav.DtoLayer.CatalogDtos.ProductsDtos;
using manav.webUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.Controllers;

public class ProductListController : Controller
{
    private const int PageSize = 9;
    private readonly IProductService _productService;

    public ProductListController(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IActionResult> Index(
        int page = 1,
        [FromQuery] List<string>? categoryIds = null)
    {
        var allProducts = new List<ResultProductWithCategoryDto>();

        if (categoryIds == null || categoryIds.Count == 0)
        {
            allProducts =
                await _productService.GetProductsWithCategoryAsync()
                ?? new List<ResultProductWithCategoryDto>();
        }
        else
        {
            foreach (var id in categoryIds.Distinct())
            {
                var products =
                    await _productService
                        .GetProductsWithCategoryByCategoryIdAsync(id);

                if (products != null)
                    allProducts.AddRange(products);
            }

            allProducts = allProducts
                .DistinctBy(p => p.ProductID)
                .ToList();
        }

        var totalPages = Math.Max(
            1, (int)Math.Ceiling(allProducts.Count / (double)PageSize));

        page = Math.Clamp(page, 1, totalPages);

        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = totalPages;
        ViewBag.SelectedCategoryIds = categoryIds ?? new List<string>();

        var productsForPage = allProducts
            .Skip((page - 1) * PageSize)
            .Take(PageSize)
            .ToList();

        return View(productsForPage);
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