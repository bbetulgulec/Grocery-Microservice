using manav.webUI.Services.UserIdentityServices;
using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.Areas.Admin.Controllers;

[Area("Admin")]
[Route("Admin/User")]
public class UserController : Controller
{

    private readonly IUserIdentityService _userIdentityService;

    public UserController(IUserIdentityService userIdentityService)
    {
        _userIdentityService = userIdentityService;
    }

    [HttpGet("GetAllUserList")]
    public async Task<IActionResult> GetAllUserList()
    {
        UserViewbagList();
        var values = await _userIdentityService.GetAllUserAsync();
        return View(values);
    }

    [HttpGet("GetUserInfo/{id}")]
    public async Task<IActionResult> GetUserInfo(string id)
    {
        UserViewbagList();
        var user = await _userIdentityService.GetUserInfo(id);
        return View(user);
    }
    
    public void UserViewbagList()
    {
        ViewBag.titles = "Kullanıcı İşlemleri";
        ViewBag.home = "Ana sayfa";
        ViewBag.table = "Kullanıcılar";
        ViewBag.tableList = "Kullanıcı Listesi";
    }
}