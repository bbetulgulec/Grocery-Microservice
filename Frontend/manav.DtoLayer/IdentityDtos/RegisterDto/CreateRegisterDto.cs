using System.ComponentModel.DataAnnotations;

namespace manav.DtoLayer.IdentityDtos.RegisterDto;

public class CreateRegisterDto
{
    public string? UserName { get; set; }

    [Required(ErrorMessage = "Required_Email")]
    [EmailAddress(ErrorMessage = "Invalid_Email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Required_Name")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Required_Surname")]
    public string SurName { get; set; }

    [Required(ErrorMessage = "Required_Password")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Required_ConfirmPassword")]
    [Compare("Password", ErrorMessage = "Password_Mismatch")]
    public string ConfimPassword { get; set; }
}