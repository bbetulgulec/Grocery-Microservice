using manav.DtoLayer.IdentityDtos.LoginDto;
using manav.webUI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.Controllers;

public class LoginController : Controller
{
    private readonly IIdentityService _identityService;

    public LoginController(IIdentityService identityService)
    {
        _identityService = identityService;
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
            ViewBag.ErrorMessage =
                "E-posta adresi veya şifre hatalı. Lütfen bilgilerinizi kontrol edip tekrar deneyin.";
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