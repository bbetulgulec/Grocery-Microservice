using manav.webUI.Services.UserIdentityServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace manav.webUI.Areas.Admin.Controllers;

[Area("Admin")]
[Route("Admin/User")]
public class UserController : Controller
{
    private readonly IStringLocalizer<SharedResource> _l;

    private readonly IUserIdentityService _userIdentityService;


    public UserController(IUserIdentityService userIdentityService, IStringLocalizer<SharedResource> l)
    {
        _userIdentityService = userIdentityService;
        _l = l;
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
        ViewBag.titles = _l["Admin_User_Title"].Value;
        ViewBag.home = _l["Common_Home"].Value;
        ViewBag.table = _l["Admin_User_Table"].Value;
        ViewBag.tableList = _l["Admin_User_TableList"].Value;
    }
}