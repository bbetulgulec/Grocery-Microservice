using manav.DtoLayer.CatalogDtos.CategoryDtos;
using manav.webUI.Services.CatalogServices.CategoryServices;
using manav.webUI.Services.ProductServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace manav.webUI.ViewComponents.ProductListViewComponents;

public class _CategoryFilterProductListComponentPartial:ViewComponent
{
    
    private readonly ICategoryService _categoryService;
    public _CategoryFilterProductListComponentPartial( ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
       var values = await _categoryService.GetAllCategoriesAsync();
        return View(values);
    }
    
}