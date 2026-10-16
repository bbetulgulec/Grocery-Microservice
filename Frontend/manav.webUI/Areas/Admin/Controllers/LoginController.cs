using manav.DtoLayer.IdentityDtos.LoginDto;
using manav.webUI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.Areas.Admin.Controllers;

[Area("Admin")]
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

        var result = await _identityService.SignInAsAdmin(createLoginDto);

        if (!result)
        {
            ModelState.AddModelError(string.Empty,
                "E-posta veya şifre hatalı ya da giriş başarısız.");

            return View(createLoginDto);
        }

        if (!User.IsInRole("Admin"))
        {
            await _identityService.Logout();

            ModelState.AddModelError(string.Empty,
                "Bu panele yalnızca Admin kullanıcıları erişebilir.");

            return View(createLoginDto);
        }

        return RedirectToAction("Index", "Category", new { area = "Admin" });
    }
}