using manav.catalog.Dtos.ProductDtos;
using manav.catalog.Services.ProductServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace manav.catalog.Contollers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ProductsController:ControllerBase
{
    
    private readonly IProductServices _productServices;

    public ProductsController(IProductServices productServices)
    {
        _productServices = productServices;
    }

    [HttpGet]
    public async Task<IActionResult> ProductList()
    {
        var values = await _productServices.GetAllCategoriesAsync();
        return Ok(values);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(string id)
    {
        var value= await _productServices.GetByIdProductAsync(id);
        return Ok(value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
    {
        await _productServices.CreateProductDto(createProductDto);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteProduct(string id)
    {
        await _productServices.DeleteProductAsync(id);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
    {
        await _productServices.UpdateProductDto(updateProductDto);
        return Ok();
    }

    [HttpGet("ProductListWithCategory")]
    public async Task<IActionResult> ProductListWithCategory()
    {
        var values = await _productServices.GetProductsWithCategoryAsync();
        return Ok(values);
    }
    [HttpGet("ProductListWithCategoryByCategoryId")]
    public async Task<IActionResult> ProductListWithCategoryByCategoryId(string categoryId)
    {
        var values = await _productServices.GetProductsWithCategoryByCategoryIdAsync(categoryId);
        return Ok(values);
    }
}