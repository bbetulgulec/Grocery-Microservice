using manav.DtoLayer.CatalogDtos.ProductsDtos;
using manav.webUI.Services;
using manav.webUI.Services.AwsS3Services;
using manav.webUI.Services.ProductServices;
using manav.webUI.Services.CatalogServices.CategoryServices;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace manav.webUI.Areas.Admin.Controllers;

[Area("Admin")]
[Route("Admin/Product")]
public class ProductsController:Controller
{
    private readonly IProductService  _productService;
    private readonly ICategoryService  _categoryServices;
    private readonly ICloudStorageService _cloudStorageService;

    public ProductsController(IProductService productService, ICategoryService categoryServices, ICloudStorageService cloudStorageService)
    {
        _categoryServices = categoryServices;
        _cloudStorageService = cloudStorageService;
        _productService = productService;
    }
    
    [Route("Index")]
    public async Task< IActionResult> Index()
    {
        ViewBagProducts();
      var values=  await _productService.GetAllProductAsync();
        return View(values);
    }
    
    [Route("ProductListWithCategory")]
    public async Task< IActionResult> ProductListWithCategory()
    {
        ViewBagProducts();
        var values = await _productService.GetProductsWithCategoryAsync();
        return View(values);
    }
    
    [Route("ProductCreate")]
    public async Task<IActionResult> CreateProduct()
    {
        ViewBagProducts();
        var values = await _categoryServices.GetAllCategoriesAsync();
        List<SelectListItem> categoryValues = (from x in values
            select new SelectListItem
            {
                Text = x.CategoryName,
                Value = x.CategoryID
            }).ToList();
        ViewBag.categoryValues = categoryValues;
        return View();
    }
    
    [HttpPost] 
    [Route("ProductCreate")]
    public async Task<IActionResult> CreateProduct(CreateProductDto value)
    {
        if (value.ImageFile != null && value.ImageFile.Length > 0)
        {
            string folderPath = "HomePage/MenuImage/MenüList";
            value.ProductImageURL = await _cloudStorageService.UploadFileAsync(value.ImageFile, folderPath);
        }
        await _productService.CreateProductDto(value);
            return RedirectToAction("Index","Products",new { area = "Admin" });
    }
    
    [Route("DeleteProduct/{id}")]
    public async Task<IActionResult> DeleteProduct(string id)
    {
       await _productService.DeleteProductAsync(id);
            return RedirectToAction("Index","Products",new { area = "Admin" });
    }
    
    [Route("UpdateProduct/{id}")]
    [HttpGet]
    public async Task<IActionResult> UpdateProduct(string id)
    {
        ViewBagProducts();
        var values = await _categoryServices.GetAllCategoriesAsync();
        List<SelectListItem> categoryValues = (from x in values
            select new SelectListItem
            {
                Text = x.CategoryName,
                Value = x.CategoryID
            }).ToList();
        ViewBag.categoryValues = categoryValues;
        var valuesProduct = await _productService.GetByIdProductAsync(id);
        return View(valuesProduct);
    }
    
    [Route("UpdateProduct/{id}")]
    [HttpPost]
    public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
    { 
        if (updateProductDto.ImageFile != null && updateProductDto.ImageFile.Length > 0)
        {
            var existingProduct = await _productService.GetByIdProductAsync(updateProductDto.ProductID);
            if (!string.IsNullOrEmpty(existingProduct?.ProductImageURL))
            {
                await _cloudStorageService.DeleteFileAsync(existingProduct.ProductImageURL);
            }
            string folderPath = "HomePage/MenuImage/MenüList";
            updateProductDto.ProductImageURL = await _cloudStorageService.UploadFileAsync(updateProductDto.ImageFile, folderPath);
        }
        await _productService.UpdateProductDto(updateProductDto);
            return RedirectToAction("Index","Products",new { area = "Admin" });
    }

    public void ViewBagProducts()
    {
        ViewBag.titles = "Ürün İşlemleri";
        ViewBag.home = "Ana sayfa";
        ViewBag.table = "Ürünler";
        ViewBag.tableList = "Ürün Listesi";
    }
}