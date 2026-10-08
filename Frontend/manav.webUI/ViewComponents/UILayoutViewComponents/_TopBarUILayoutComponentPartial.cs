using System.Security.Claims;
using manav.webUI.Services.UserIdentityServices;
using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.ViewComponents.UILayoutViewComponents;

public class _TopBarUILayoutComponentPartial : ViewComponent
{
    private readonly IUserIdentityService _identityService;

    public _TopBarUILayoutComponentPartial(IUserIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        if (UserClaimsPrincipal.Identity?.IsAuthenticated == true)
        {
            var userId = UserClaimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value
                         ?? UserClaimsPrincipal.FindFirst("sub")?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                var userInfo = await _identityService.GetUserInfo(userId);
                return View(userInfo);
            }
        }

        ViewData.Model = null;
        return View();

    }
}