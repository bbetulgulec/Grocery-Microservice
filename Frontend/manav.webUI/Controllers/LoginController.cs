using manav.DtoLayer.IdentityDtos.LoginDto;
using manav.webUI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.Controllers;

public class LoginController : Controller
{
    private  readonly IIdentityService _identityService;
    public LoginController( IIdentityService identityService)
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
        await  _identityService.SignIn(createLoginDto);
        return RedirectToAction("Index", "Default");
    }
    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await   _identityService.Logout();
        return RedirectToAction("Index", "Default");

    }

}