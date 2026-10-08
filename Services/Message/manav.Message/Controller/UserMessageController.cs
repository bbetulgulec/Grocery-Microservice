using AutoMapper;
using manav.Message.Dtos;
using manav.Message.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace manav.Message.Controller;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserMessageController : ControllerBase
{
    private readonly IMessageService  _messageService;

    public UserMessageController(IMessageService messageService)
    {
        _messageService = messageService;
    }

    [HttpGet]
    public async Task<IActionResult> GetMessageAsync()
    {
        var values = await _messageService.GetAllMessageAsync();
        return Ok(values);
    }

    [HttpGet("GetMessageInbox")]
    public async Task<IActionResult> GetMessageInboxAsync(string id)
    {
        var values = await _messageService.GetAllMessageInboxAsync(id);
        return Ok(values);
    }
    [HttpGet("GetMessageSendbox")]
    public async Task<IActionResult> GetMessageSenboxAsync(string id)
    {
        var values = await _messageService.GetAllMessageSendboxAsync(id);
        return Ok(values);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMessageAsync(CreateMessageDto createMessageDto)
    {
         await _messageService.CreateMessageAsync(createMessageDto);
         return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteMessageAsync(int id)
    {
        await _messageService.DeleteMessageAsync(id);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateMessageAsync(UpdateMessageDto updateMessageDto)
    {
        await _messageService.UpdateMessageAsync(updateMessageDto);
        return Ok();
    }
}