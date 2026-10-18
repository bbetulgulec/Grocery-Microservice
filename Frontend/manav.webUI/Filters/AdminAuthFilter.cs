using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace manav.webUI.Filters;

public class AdminAuthFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var area = context.RouteData.Values["area"]?.ToString();
        if (!string.Equals(area, "Admin", StringComparison.OrdinalIgnoreCase))
            return;

        var controller = context.RouteData.Values["controller"]?.ToString();

        // Admin login sayfasına herkes girebilsin (döngü olmasın)
        if (string.Equals(controller, "Login", StringComparison.OrdinalIgnoreCase))
            return;

        var isAdmin = context.HttpContext.User.HasClaim("is_admin", "true");
        if (!isAdmin)
            context.Result = new RedirectResult("/Admin/Login/Index");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}