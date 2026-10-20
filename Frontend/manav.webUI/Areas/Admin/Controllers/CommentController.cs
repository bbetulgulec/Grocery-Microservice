using manav.DtoLayer.CommentDtos.CommentDtos;
using manav.webUI.Services.CatalogServices.CommentServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace manav.webUI.Areas.Admin.Controllers;

[Area("Admin")]
[Route("Admin/Comment")]
public class CommentController : Controller
{
    private readonly IAdminCommentService _commentService;
    private readonly IStringLocalizer<SharedResource> _l;


    public CommentController(IAdminCommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpGet]
    [Route("Index")]
    public async Task<IActionResult> Index()
    {
        ViewbagComment();
        var values = await _commentService.GetAllCommentAsync();
        return View(values);
    }

    [HttpGet]
    [Route("CreateComment")]
    public IActionResult CreateComment()
    {
        ViewbagComment();
        return View();
    }

    [HttpPost]
    [Route("CreateComment")]
    public async Task<IActionResult> CreateComment(CreateCommentDto value)
    {
        await _commentService.CreateCommentDto(value);
        return RedirectToAction("Index", "Comment", new { area = "Admin" });
    }

    [HttpGet]
    [Route("DeleteComment/{id}")]
    public async Task<IActionResult> DeleteComment(int id)
    {
        await _commentService.DeleteCommentAsync(id);
        return RedirectToAction("Index", "Comment", new { area = "Admin" });
    }

    [HttpPost]
    [Route("UpdateComment")]
    public async Task<IActionResult> UpdateComment(UpdateCommentDto updateCommentDto)
    {
        await _commentService.UpdateCommentDto(updateCommentDto);
        return View();
    }

    [HttpGet]
    [Route("FetchGoogleReviews")]
    public async Task<IActionResult> FetchGoogleReviews()
    {
        await _commentService.SyncGoogleReviewsAsync();
        return RedirectToAction("Index", "Comment", new { area = "Admin" });
    }


    [HttpGet]
    [Route("ChangeCommentStatus/{id}")]
    public async Task<IActionResult> ChangeCommentStatus(int id)
    {
        await _commentService.ChangeCommentStatusAsync(id);
        return RedirectToAction("Index", "Comment", new { area = "Admin" });
    }

    public void ViewbagComment()
    {
        ViewBag.titles = _l["Admin_Comment_Title"].Value;
        ViewBag.home = _l["Common_Home"].Value;
        ViewBag.table = _l["Admin_Comment_Table"].Value;
        ViewBag.tableList = _l["Admin_Comment_TableList"].Value;
    }
}