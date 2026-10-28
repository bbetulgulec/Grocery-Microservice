using manav.webUI.Services.CatalogServices.CategoryServices;
using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.ViewComponents.ProductListViewComponents;

public class _CategoryFilterProductListComponentPartial : ViewComponent
{
    private readonly ICategoryService _categoryService;

    public _CategoryFilterProductListComponentPartial(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var values = await _categoryService.GetAllCategoriesAsync();
        return View(values);
    }
}