using manav.DtoLayer.IdentityDtos.LoginDto;
using manav.DtoLayer.IdentityDtos.RegisterDto;

namespace manav.webUI.Services.Interfaces;

public interface IIdentityService
{
    Task<bool> SignIn(CreateLoginDto createLoginDto);
    Task<bool> SignUp(CreateRegisterDto createRegisterDto);
    Task<bool> GetRefreshToken();
    Task<bool> Logout();

}