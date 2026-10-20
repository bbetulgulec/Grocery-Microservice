using manav.DtoLayer.IdentityDtos.LoginDto;
using manav.webUI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace manav.webUI.Controllers;

public class LoginController : Controller
{
    private readonly IIdentityService _identityService;
    private readonly IStringLocalizer<SharedResource> _l;


    public LoginController(IIdentityService identityService, IStringLocalizer<SharedResource> l)
    {
        _identityService = identityService;
        _l = l;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(CreateLoginDto createLoginDto)
    {
        if (!ModelState.IsValid)
            return View(createLoginDto);

        var result = await _identityService.SignIn(createLoginDto);

        if (!result)
        {
            ViewBag.ErrorMessage = _l["Login_Error"].Value;
            return View(createLoginDto);
        }

        return RedirectToAction("Index", "Default");
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _identityService.Logout();
        return RedirectToAction("Index", "Default");
    }
}