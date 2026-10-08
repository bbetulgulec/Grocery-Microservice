using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.ViewComponents.UILayoutViewComponents;

public class _PopUpEntryCommentComponentPartial:ViewComponent
{
    public IViewComponentResult Invoke()
    {
        string placeId = "ChIJF05-oZiwyhQRv5TANBZ7M_I";
        ViewBag.GoogleReviewUrl = $"https://search.google.com/local/writereview?placeid={placeId}";
        return View();
    }
    
}