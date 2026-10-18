using System.ComponentModel.DataAnnotations;

namespace manav.DtoLayer.IdentityDtos.RegisterDto;

public class CreateRegisterDto
{
    public string? UserName { get; set; }

    [Required(ErrorMessage = "E-posta alanı zorunludur.")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi girin.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "İsim alanı zorunludur.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Soyisim alanı zorunludur.")]
    public string SurName { get; set; }

    [Required(ErrorMessage = "Şifre alanı zorunludur.")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Şifre tekrarı zorunludur.")]
    [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor.")]
    public string ConfimPassword { get; set; }
}