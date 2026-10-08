using manav.catalog.Dtos.ProductDetailsDtos;
using manav.catalog.Services.ProductDetailServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace manav.catalog.Contollers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ProductDetailContoller:ControllerBase
{
    
    private readonly IProductDetailServices _productDetailServices;

    public ProductDetailContoller(IProductDetailServices productDetailServices)
    {
        _productDetailServices = productDetailServices;
    }

    [HttpGet]
    public async Task<IActionResult> ProductDetailList()
    {
        var values = await _productDetailServices.GetAllProductDetailAsync();
        return Ok(values);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductDetailById(string id)
    {
        var value= await _productDetailServices.GetByIdProductDetailAsync(id);
        return Ok(value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductDetail(CreateProductDetailsDto createProductDetailDto)
    {
        await _productDetailServices.CreateProductDetailAsync(createProductDetailDto);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteProductDetail(string id)
    {
        await _productDetailServices.DeleteProductDetailAsync(id);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProductDetail(UpdateProductDetailDto updateProductDetailDto)
    {
        await _productDetailServices.UpdateProductDetailAsync(updateProductDetailDto);
        return Ok();
    }
}