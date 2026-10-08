using System.Text;
using manav.webUI.Services.CatalogServices.CategoryServices;
using manav.DtoLayer.CatalogDtos.CategoryDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace manav.webUI.Areas.Admin.Controllers;

[Area("Admin")]
[Route("Admin/Category")]
public class CategoryController : Controller
{

    private readonly ICategoryService _categoryServices;
    public CategoryController( ICategoryService categoryServices)
    {
        _categoryServices = categoryServices;
    }
    [Route("Index")]
    public async Task< IActionResult> Index()
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
   await   _categoryServices.CreateCategoryAsync(value);
            return RedirectToAction("Index","Category",new { area = "Admin" });
    }

    [Route("DeleteCategory/{id}")]
    public async Task<IActionResult> DeleteCategory(string id)
    {
     await  _categoryServices.DeleteCategoryAsync(id);
            return RedirectToAction("Index","Category",new { area = "Admin" });
      
    }

    [Route("UpdateCategory/{id}")]
    [HttpGet]
    public async Task<IActionResult> UpdateCategory(string id)
    {
        CategoryViewbagList();
     var values=await   _categoryServices.GetByIdCategoryAsync(id);
        return View(values);
    }
    [Route("UpdateCategory/{id}")]
    [HttpPost]
    public async Task<IActionResult> UpdateCategory(UpdateCategoryDtos updateCategoryDtos)
    {
    
       await  _categoryServices.UpdateCategoryAsync(updateCategoryDtos);
        return RedirectToAction("Index","Category",new { area = "Admin" });
    }
    public void CategoryViewbagList()
    {
        ViewBag.titles = "Kategori İşlemleri";
        ViewBag.home = "Ana sayfa";
        ViewBag.table = "Kategoriler";
        ViewBag.tableList = "Kategori Listesi";
    }
}