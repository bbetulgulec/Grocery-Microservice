using manav.discount.Dtos;
using manav.discount.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace manav.discount.Controller;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class DiscountController:ControllerBase
{
    private readonly IDiscountServices _discountServices;

    public DiscountController(IDiscountServices discountServices)
    {
        _discountServices = discountServices;
    }
    
    [HttpGet]
    public async Task<IActionResult> CouponList()
    {
        var values = await _discountServices.GetAllCouponAsync();
        return Ok(values);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCoupons(String id)
    {
        var value = await _discountServices.GetByIdCouponAsync(id);
        return Ok(value);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCoupons(String id)
    {
        await _discountServices.DeleteCouponAsync(id);
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> CreateCoupon(CreateCouponDto dto)
    {
        await _discountServices.CreateCouponAsync(dto);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCoupon(UpdateCouponDto dto)
    {
        await _discountServices.UpdateCouponAsync(dto);
        return Ok();
    }
}