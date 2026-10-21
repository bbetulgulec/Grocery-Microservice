using manav.DtoLayer.CatalogDtos.ProductsDtos;
using manav.webUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.ViewComponents.ProductListViewComponents;

public class _ProductsProductListComponentPartial : ViewComponent
{
    private readonly IProductService _productService;

    public _ProductsProductListComponentPartial(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IViewComponentResult> InvokeAsync(List<ResultProductWithCategoryDto>? products = null)
    {
        if (products == null)
            products = await _productService.GetProductsWithCategoryAsync() ?? new List<ResultProductWithCategoryDto>();

        return View(products);
    }
}