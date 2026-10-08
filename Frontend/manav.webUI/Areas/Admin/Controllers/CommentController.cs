using manav.DtoLayer.CommentDtos.CommentDtos;
using manav.webUI.Services.CatalogServices.CommentServices;
using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.Areas.Admin.Controllers;

[Area("Admin")]
[Route("Admin/Comment")]
public class CommentController:Controller
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }
        [HttpGet]
    [Route("Index")]
    public async Task<IActionResult> Index()
    {
        ViewbagComment();
       var values= await _commentService.GetAllCommentAsync();
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
        ViewBag.titles = "Yorum İşlemleri";
        ViewBag.home = "Ana sayfa";
        ViewBag.table = "Yorumlar";
        ViewBag.tableList = "Yorum Bilgileri";
    }
}