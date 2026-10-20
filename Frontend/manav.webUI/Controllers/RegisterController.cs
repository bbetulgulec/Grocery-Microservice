using manav.DtoLayer.IdentityDtos.RegisterDto;
using manav.webUI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace manav.webUI.Controllers;

public class RegisterController : Controller
{
    private readonly IIdentityService _identityService;
    private readonly IStringLocalizer<SharedResource> _l;


    public RegisterController(IIdentityService identityService, IStringLocalizer<SharedResource> l)
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
    public async Task<IActionResult> Index(CreateRegisterDto createRegisterDto)
    {
        if (!ModelState.IsValid)
            return View(createRegisterDto);

        if (string.IsNullOrWhiteSpace(createRegisterDto.UserName))
            createRegisterDto.UserName = createRegisterDto.Email;
        var registered = await _identityService.SignUp(createRegisterDto);

        if (!registered)
        {
            ViewBag.ErrorMessage = _l["Register_Error"].Value;
            return View(createRegisterDto);
        }

        return RedirectToAction("Index", "Login");
    }
}