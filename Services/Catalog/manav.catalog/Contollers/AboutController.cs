using manav.catalog.Dtos.AboutDtos;
using manav.catalog.Services.AboutServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace manav.catalog.Contollers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AboutController : ControllerBase
{
    
    private readonly IAboutServices _aboutServices;

    public AboutController(IAboutServices aboutServices)
    {
        _aboutServices = aboutServices;
    }

    [HttpGet]
    public async Task<IActionResult> GetAbout()
    {
        var values = await _aboutServices.GetAboutAsync();
        return Ok(values);
    }
    

    [HttpPost]
    public async Task<IActionResult> CreateAbout(CreateAboutDto createAboutDto)
    {
        await _aboutServices.CreateAboutAsync(createAboutDto);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAbout(string id)
    {
        await _aboutServices.DeleteAboutAsync(id);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAbout(UpdateAboutDto updateAboutDto)
    {
        await _aboutServices.UpdateAboutAsync(updateAboutDto);
        return Ok();
    }
}