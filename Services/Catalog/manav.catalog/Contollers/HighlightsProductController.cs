using AutoMapper;
using manav.catalog.Dtos.HighlightsProductDtos;
using manav.catalog.Services.HighlightsProductsServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace manav.catalog.Contollers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class HighlightsProductController:ControllerBase
{
    private readonly IHighlightsProductServices _highlightsProductServices;

    public HighlightsProductController(IHighlightsProductServices highlightsProductServices)
    {
        _highlightsProductServices = highlightsProductServices;
    }
    [HttpGet]
    public async Task<IActionResult> HighlightsProductList()
    {
        var values = await _highlightsProductServices.GetAllHighlightsProductAsync();
        return Ok(values);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetHighlightsProductById(string id)
    {
        var value= await _highlightsProductServices.GetByIdHighlightsProductAsync(id);
        return Ok(value);
    }
    [HttpGet("HighlightsProductListWithProduct")]
    public async Task<IActionResult> HighlightsProductListWithProduct()
    {
        var values = await _highlightsProductServices.GetHighlightsProductListWithProductAsync();
        return Ok(values);
    }

    [HttpPost]
    public async Task<IActionResult> CreateHighlightsProduct(CreateHighlightsProductDto createHighlightsProductDto)
    {
        await _highlightsProductServices.CreateHighlightsProductAsync(createHighlightsProductDto);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteHighlightsProductDto(string id)
    {
        await _highlightsProductServices.DeleteHighlightsProductAsync(id);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateHighlightsProductDto(UpdateHighlightsProductDto updateHighlightsProductDto)
    {
        await _highlightsProductServices.UpdateHighlightsProductAsync(updateHighlightsProductDto);
        return Ok();
    }
}