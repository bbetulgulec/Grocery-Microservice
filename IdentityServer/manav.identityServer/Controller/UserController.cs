using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityServer4;
using manav.identityServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;

namespace manav.identityServer.Controller;

[Authorize(IdentityServerConstants.LocalApi.PolicyName)]
[ApiController]
[Route("api/[controller]")]
public class UserController:ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet("GetUser")]
    public async Task<IActionResult> GetUser()
    {
        var userClaim=User.Claims.FirstOrDefault(x=>x.Type==JwtRegisteredClaimNames.Sub);
        var user= await _userManager.FindByIdAsync(userClaim.Value);
        return Ok(new
        {
            Id=user.Id,
            Name=user.UserName,
            Surname=user.UserName,
            email=user.Email,
            username=user.UserName,
        });
    }

    [HttpGet("GetAllUserList")]
    public async Task<IActionResult> GetAllUserList()
    {
        var value = await _userManager.Users.ToListAsync();
        return Ok(value);
    }
    
    [HttpGet("GetUserInfo/{id}")]
    public async Task<IActionResult> GetUserInfo(string id)
    {
        var value = await _userManager.FindByIdAsync(id);
        return Ok(value);
    }
}