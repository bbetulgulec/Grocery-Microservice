using manav.catalog.Dtos.ProductImageDtos;
using manav.catalog.Services.ProductImageServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace manav.catalog.Contollers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ProductImageController:ControllerBase
{
      
    private readonly IProductImageServices _productImageServices;

    public ProductImageController(IProductImageServices productImageServices)
    {
        _productImageServices = productImageServices;
    }

    [HttpGet]
    public async Task<IActionResult> ProductImageList()
    {
        var values = await _productImageServices.GetAllProductImagAsync();
        return Ok(values);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductImageById(string id)
    {
        var value= await _productImageServices.GetByIdPsroductImageAsync(id);
        return Ok(value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductImage(CreateProductImageDto createProductImageDto)
    {
        await _productImageServices.CreateProductImageAsync(createProductImageDto);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteProductImage(string id)
    {
        await _productImageServices.DeleteProductImageAsync(id);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProductImage(UpdateProductImageDto updateProductImageDto)
    {
        await _productImageServices.UpdateProductImageAsync(updateProductImageDto);
        return Ok();
    }
}