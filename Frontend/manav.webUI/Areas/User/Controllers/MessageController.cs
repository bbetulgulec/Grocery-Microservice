using AutoMapper;
using manav.webUI.Services.Interfaces;
using manav.webUI.Services.MessageServices;
using Microsoft.AspNetCore.Mvc;

namespace manav.webUI.Areas.User.Controllers;

[Area("User")]
[Route("User/Message")]
public class MessageController : Controller
{
    private readonly IMessageService _messageService;
    private readonly IUserService _userService;

    public MessageController(IMessageService messageService, IUserService userService)
    {
        _messageService = messageService;
        _userService = userService;
    }

    public async Task<IActionResult> Inbox()
    {
        var user= await _userService.GetUser();
        var values = await _messageService.GetAllMessageInboxAsync(user.Id);
        return View(values);
    }
    public async Task<IActionResult> Sendbox()
    {
        var user= await _userService.GetUser();
        var values = await _messageService.GetAllMessageSendboxAsync(user.Id);
        return View(values);
    }
}