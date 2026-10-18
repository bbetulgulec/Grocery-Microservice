using manav.DtoLayer.IdentityDtos.RegisterDto;
using manav.webUI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.Controllers;

public class RegisterController : Controller
{
    private readonly IIdentityService _identityService;

    public RegisterController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(CreateRegisterDto createRegisterDto)
    {
        if (!ModelState.IsValid)
            return View(createRegisterDto);

        if (string.IsNullOrWhiteSpace(createRegisterDto.UserName))
            createRegisterDto.UserName = createRegisterDto.Email;
        var registered = await _identityService.SignUp(createRegisterDto);

        if (!registered)
        {
            ViewBag.ErrorMessage =
                "Kayıt oluşturulamadı. Bu e-posta adresi zaten kayıtlı olabilir veya şifreniz kurallara uymuyor olabilir.";
            return View(createRegisterDto);
        }

        return RedirectToAction("Index", "Login");
    }
}