using System.Text;
using FluentValidation;
using manav.DtoLayer.CatalogDtos.AboutDtos;
using manav.webUI.Services.CatalogServices.AboutServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace manav.webUI.Areas.Admin.Controllers;

[Area("Admin")]
[Route("Admin/About")]
public class AboutController : Controller
{
    private readonly IValidator<CreateAboutDto> _validator;
    private readonly IAboutService _aboutService;

    public AboutController( IValidator<CreateAboutDto> validator, IAboutService aboutService)
    {
        _validator = validator;
        _aboutService = aboutService;
    }
    [HttpGet]
    [Route("Index")]
    public async Task<IActionResult> Index()
    {
        var values = await _aboutService.GetAboutAsync();

        if (values == null)
        {
            return RedirectToAction("CreateAbout","About",new { area = "Admin" });
        }

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
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

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
        await  _aboutService.UpdateAboutAsync(updateAboutDto);
        return RedirectToAction("Index", "About", new { area = "Admin" });
    }

    public void ViewbagAbout()
    {
        ViewBag.titles = "Hakkında İşlemleri";
        ViewBag.home = "Ana sayfa";
        ViewBag.table = "Hakkında";
        ViewBag.tableList = "Hakkında Bilgileri";
    }
}