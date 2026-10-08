using manav.webUI.Services.CatalogServices.CommentServices;
using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.ViewComponents.UILayoutViewComponents;

public class _TestimonialsUILayoutComponentPartial:ViewComponent
{
    private readonly ICommentService _commentService;
    public _TestimonialsUILayoutComponentPartial(ICommentService commentService)
    {
        _commentService = commentService;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var values = await _commentService.GetActiveCommentsAsync();
        return View(values);
    }
}