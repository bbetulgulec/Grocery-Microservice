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
        if (createRegisterDto.Password == createRegisterDto.ConfimPassword)
        {
            var registered = await _identityService.SignUp(createRegisterDto);

            if (registered)
                return RedirectToAction("Index", "Login");
        }

        return View(createRegisterDto);
    }
}