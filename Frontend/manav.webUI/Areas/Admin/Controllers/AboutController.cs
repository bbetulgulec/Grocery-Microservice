using FluentValidation;
using manav.DtoLayer.CatalogDtos.AboutDtos;
using manav.webUI.Services.CatalogServices.AboutServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace manav.webUI.Areas.Admin.Controllers;

[Area("Admin")]
[Route("Admin/About")]
public class AboutController : Controller
{
    private readonly IAboutService _aboutService;
    private readonly IStringLocalizer<SharedResource> _l;
    private readonly IValidator<CreateAboutDto> _validator;


    public AboutController(IValidator<CreateAboutDto> validator, IAboutService aboutService,
        IStringLocalizer<SharedResource> l)
    {
        _validator = validator;
        _aboutService = aboutService;
        _l = l;
    }

    [HttpGet]
    [Route("Index")]
    public async Task<IActionResult> Index()
    {
        ViewbagAbout();
        var values = await _aboutService.GetAboutAsync();

        if (values == null) return RedirectToAction("CreateAbout", "About", new { area = "Admin" });

        return View(values);
    }

    [HttpGet]
    [Route("CreateAbout")]
    public async Task<IActionResult> CreateAbout()
    {
        ViewbagAbout();
        await _aboutService.GetAboutAsync();
        return View();
    }

    [HttpPost]
    [Route("CreateAbout")]
    public async Task<IActionResult> CreateAbout(CreateAboutDto value)
    {
        var validationResult = await _validator.ValidateAsync(value);
        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

            return View(value);
        }

        await _aboutService.CreateAboutAsync(value);
        return RedirectToAction("Index", "About", new { area = "Admin" });
    }

    [HttpGet]
    [Route("DeleteAbout/{id}")]
    public async Task<IActionResult> DeleteAbout(string id)
    {
        await _aboutService.DeleteAboutAsync(id);
        return RedirectToAction("CreateAbout", "About", new { area = "Admin" });
    }

    [HttpPost]
    [Route("UpdateAbout")]
    public async Task<IActionResult> UpdateAbout(UpdateAboutDto updateAboutDto)
    {
        await _aboutService.UpdateAboutAsync(updateAboutDto);
        return RedirectToAction("Index", "About", new { area = "Admin" });
    }

    public void ViewbagAbout()
    {
        ViewBag.titles = _l["Admin_About_Title"].Value;
        ViewBag.home = _l["Common_Home"].Value;
        ViewBag.table = _l["Admin_About_Table"].Value;
        ViewBag.tableList = _l["Admin_About_TableList"].Value;
    }
}