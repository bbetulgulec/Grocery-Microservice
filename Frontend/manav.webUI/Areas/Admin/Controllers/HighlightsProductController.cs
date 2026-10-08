using manav.DtoLayer.CatalogDtos.HighlightsProductDtos;
using manav.webUI.Services;
using manav.webUI.Services.AwsS3Services;
using manav.webUI.Services.CatalogServices.HighlightsProductServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace manav.webUI.Areas.Admin.Controllers;

[Area("Admin")]
[Route("Admin/HighlightsProduct")]
public class HighlightsProductController : Controller
{
    private readonly IHighlightsProductService _highlightsProductService;
    private readonly IProductService _productService;
    private readonly ICloudStorageService  _cloudStorageService;
    public HighlightsProductController(IHighlightsProductService highlightsProductService, IProductService productService, ICloudStorageService cloudStorageService)
    {
        _highlightsProductService = highlightsProductService;
        _productService = productService;
        _cloudStorageService = cloudStorageService;
    }

    [Route("Index")]
    public async Task< IActionResult> Index()
    {
        ViewbagHighlightProduct();
        var values = await _highlightsProductService.GetAllHighlightsProductAsync();
        return View();
    }
    
    [Route("HighlightsProductListWithProduct")]
    public async Task<IActionResult> HighlightsProductListWithProduct()
    {
        ViewbagHighlightProduct();
        var values = await _highlightsProductService.GetHighlightsProductListWithProductAsync();
        return View(values);
    }
    
    [HttpGet]
    [Route("CreateHighlightsProduct")]
    public async Task<IActionResult> CreateHighlightsProduct()
    {
        ViewbagHighlightProduct();
        var productList = await _productService.GetAllProductAsync();
        if (productList != null && productList.Any())
        {
            ViewBag.ProductList = (from x in productList
                select new SelectListItem
                {
                    Text = x.ProductName,
                    Value = x.ProductID
                }).ToList();
        }
        else
        {
            ViewBag.ProductList = new List<SelectListItem>();
        }
        return View();
    }
    
    [HttpGet]
    [Route("GetProductDetailForHighlight")]
    public async Task<IActionResult> GetProductDetailForHighlight(string id)
    {
        var product =   await _productService.GetByIdProductAsync(id);
        return Json(product);
    }
    
    [HttpPost]
    [Route("CreateHighlightsProduct")]
    public async Task<IActionResult> CreateHighlightsProduct(CreateHighlightsProductDto value)
{
  
    if (value.Product.ImageFile != null && value.Product.ImageFile.Length > 0)
    {
        string folderPath = "HomePage/MenuImage/MenüList";
        value.Product.ProductImageURL = await _cloudStorageService.UploadFileAsync(value.Product.ImageFile, folderPath);
    }
        await _highlightsProductService.CreateHighlightsProductAsync(value);
    return RedirectToAction("HighlightsProductListWithProduct","HighlightsProduct",new { area = "Admin" });
    }
    
    [Route("DeleteHighlightsProduct/{id}")]
    public async Task<IActionResult> DeleteHighlightsProduct(string id)
    {
        await _highlightsProductService.DeleteHighlightsProductAsync(id);
        return RedirectToAction("HighlightsProductListWithProduct","HighlightsProduct",new { area = "Admin" });
    }
    
    [Route("UpdateHighlightsProduct/{id}")]
    [HttpGet]
    public async Task<IActionResult> UpdateHighlightsProduct(string id)
    {
        ViewbagHighlightProduct();
        var productList = await _productService.GetAllProductAsync();
        var highlights = await _highlightsProductService
            .GetHighlightsProductListWithProductAsync();
        var highlight = highlights.FirstOrDefault(x =>
            x.HighlightsProductsID == id);
        var selectedProductId = highlight.Product?.ProductID;
        if (productList != null && productList.Any())
        {
            ViewBag.ProductList = productList.Select(x => new SelectListItem
            {
                Text = x.ProductName,
                Value = x.ProductID,
                Selected = x.ProductID == selectedProductId
            }).ToList();
        }
        var values = new UpdateHighlightsProductDto
        {
            HighlightsProductsID = highlight.HighlightsProductsID,
            HighlightsProductsName = highlight.HighlightsProductsName,
            Product = new DtoLayer.CatalogDtos.ProductsDtos.UpdateProductDto()
            {
                ProductID = selectedProductId
            }
        };
        return View(values);
    }
    
    [Route("UpdateHighlightsProduct/{id}")]
    [HttpPost]
    public async Task<IActionResult> UpdateHighlightsProduct(UpdateHighlightsProductDto updateHighlightsProductDto)
    {
        if (updateHighlightsProductDto.Product.ImageFile != null && updateHighlightsProductDto.Product.ImageFile.Length > 0)
        {
            var existingProduct = await _productService.GetByIdProductAsync(updateHighlightsProductDto.Product.ProductID);
            if (!string.IsNullOrEmpty(existingProduct?.ProductImageURL))
            {
                await _cloudStorageService.DeleteFileAsync(existingProduct.ProductImageURL);
            }
            string folderPath = "HomePage/MenuImage/MenüList";
            updateHighlightsProductDto.Product.ProductImageURL = await _cloudStorageService.UploadFileAsync(updateHighlightsProductDto.Product.ImageFile, folderPath);
        }
     await _highlightsProductService.UpdateHighlightsProductAsync(updateHighlightsProductDto);
         return RedirectToAction("HighlightsProductListWithProduct","HighlightsProduct",new { area = "Admin" });
    }
    
    public void ViewbagHighlightProduct() { 
        ViewBag.titles = "Öne Çıkarılan Ürünlerin İşlemleri";
        ViewBag.home = "Ana sayfa";
        ViewBag.table = "Öne çıkarılan ürünler";
        ViewBag.tableList = "Öne çıkarılan ürünler Listesi";
    }
}