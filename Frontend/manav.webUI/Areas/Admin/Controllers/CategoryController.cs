using manav.DtoLayer.CatalogDtos.CategoryDtos;
using manav.webUI.Services.CatalogServices.CategoryServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace manav.webUI.Areas.Admin.Controllers;

[Area("Admin")]
[Route("Admin/Category")]
public class CategoryController : Controller
{
    private readonly ICategoryService _categoryServices;
    private readonly IStringLocalizer<SharedResource> _l;

    public CategoryController(ICategoryService categoryServices, IStringLocalizer<SharedResource> l)
    {
        _categoryServices = categoryServices;
        _l = l;
    }

    [Route("Index")]
    public async Task<IActionResult> Index()
    {
        CategoryViewbagList();
        var values = await _categoryServices.GetAllCategoriesAsync();
        return View(values);
    }

    [HttpGet]
    [Route("CreateCategory")]
    public IActionResult CreateCategory()
    {
        CategoryViewbagList();
        return View();
    }

    [HttpPost]
    [Route("CreateCategory")]
    public async Task<IActionResult> CreateCategory(CreateCategoryDtos value)
    {
        await _categoryServices.CreateCategoryAsync(value);
        return RedirectToAction("Index", "Category", new { area = "Admin" });
    }

    [Route("DeleteCategory/{id}")]
    public async Task<IActionResult> DeleteCategory(string id)
    {
        await _categoryServices.DeleteCategoryAsync(id);
        return RedirectToAction("Index", "Category", new { area = "Admin" });
    }

    [Route("UpdateCategory/{id}")]
    [HttpGet]
    public async Task<IActionResult> UpdateCategory(string id)
    {
        CategoryViewbagList();
        var values = await _categoryServices.GetByIdCategoryAsync(id);
        return View(values);
    }

    [Route("UpdateCategory/{id}")]
    [HttpPost]
    public async Task<IActionResult> UpdateCategory(UpdateCategoryDtos updateCategoryDtos)
    {
        await _categoryServices.UpdateCategoryAsync(updateCategoryDtos);
        return RedirectToAction("Index", "Category", new { area = "Admin" });
    }

    public void CategoryViewbagList()
    {
        ViewBag.titles = _l["Admin_Category_Title"].Value;
        ViewBag.home = _l["Common_Home"].Value;
        ViewBag.table = _l["Admin_Category_Table"].Value;
        ViewBag.tableList = _l["Admin_Category_TableList"].Value;
    }
}