using System.Threading.Tasks;
using manav.identityServer.Dtos;
using manav.identityServer.Models;
using manav.identityServer.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace manav.identityServer.Controller;

[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class LoginController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    : ControllerBase
{
    private readonly IConfiguration _configuration;


    [HttpPost]
    public async Task<IActionResult> UserLogin([FromBody] UserLoginDto userLoginDto)
    {
        var result = await signInManager.PasswordSignInAsync(
            userLoginDto.Email,
            userLoginDto.Password,
            false,
            false
        );
        var user = await userManager.FindByEmailAsync(userLoginDto.Email);
        if (result.Succeeded)
        {
            var model = new GetCheckAppUserViewModel();
            model.Username = userLoginDto.Email;
            model.Id = user.Id;
            var token = JwtTokenGenerator.GenerateToken(model, _configuration);
            return Ok(token);
        }

        return Ok();
    }
}