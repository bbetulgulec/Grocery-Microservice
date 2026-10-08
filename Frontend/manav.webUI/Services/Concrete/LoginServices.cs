using System.Security.Claims;

namespace manav.webUI.Services;

public class LoginServices:ILoginServices
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LoginServices(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetUserId=>_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
}