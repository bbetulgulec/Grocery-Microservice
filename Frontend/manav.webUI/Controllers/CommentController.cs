using manav.DtoLayer.CommentDtos.CommentDtos;
using manav.webUI.Services.CatalogServices.CommentServices;
using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.Controllers;

public class CommentController : Controller
{
    private readonly ICommentService _commentService;


    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpPost]
    public async Task<IActionResult> AddComment(CreateCommentDto createCommentDto)
    {
        await _commentService.CreateCommentDto(createCommentDto);

        TempData["ShowThankYouModal"] = true;
        TempData["UserComment"] = createCommentDto.CommentDetail;

        var referer = Request.Headers.Referer.ToString();
        return RedirectToAction("Index", "Default");
    }
}