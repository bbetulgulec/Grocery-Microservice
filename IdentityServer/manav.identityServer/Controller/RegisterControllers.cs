using System;
using System.Threading.Tasks;
using manav.identityServer.Dtos;
using manav.identityServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace manav.identityServer.Controller;

[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class RegisterControllers : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;

    public RegisterControllers(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> UserRegister(UserRegisterDto userRegisterDto)
    {
        var values = new ApplicationUser
        {
            Email = userRegisterDto.Email,
            Name = userRegisterDto.Name,
            Surname = userRegisterDto.SurName,
            UserName = userRegisterDto.Email
        };

        var result = await _userManager.CreateAsync(values, userRegisterDto.Password);
        if (result.Succeeded)
        {
            if (userRegisterDto.Email.Equals("mevsimmanav@gmail.com", StringComparison.OrdinalIgnoreCase))
                await _userManager.AddToRoleAsync(values, "Admin");
            return Ok(values);
        }

        return BadRequest(result.Errors);
    }
}