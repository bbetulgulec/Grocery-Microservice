
using manav.component.Dtos;
using manav.component.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace manav.component.Controller;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CommentController:ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpGet]
    public async Task<IActionResult> CommentList()
    {
        var values =await _commentService.GetAllCommentAsync(); 
        return Ok(values);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetComment(int id)
    {
        var value =await _commentService.GetByIdCommentAsync(id);
        return Ok(value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateComment(CreateCommentDto comment)
    {
        await _commentService.CreateCommentDto(comment);
       return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateComment(UpdateCommentDto comment)
    {
       await _commentService.UpdateCommentDto(comment);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteComment(int id)
    {
         await _commentService.DeleteCommentAsync(id);
        return Ok();
    }
  
    [HttpPost("FetchGoogleReviews")]
    public async Task<IActionResult> FetchGoogleReviews()
    {
        await _commentService.SyncGoogleReviewsAsync();
        return Ok(new { message = "Google yorumları başarıyla senkronize edildi." });
    }
    
    [HttpGet("ChangeCommentStatus/{id}")]
    public async Task<IActionResult> ChangeCommentStatus(int id)
    {
        await _commentService.ChangeCommentStatusAsync(id);
        return Ok();
    }
    [HttpGet("GetActiveComments")]
    public async Task<IActionResult> GetActiveComments()
    {
        var values = await _commentService.GetActiveCommentsAsync(); 
        return Ok(values);
    }
    
}