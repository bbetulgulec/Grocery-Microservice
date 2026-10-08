using manav.DtoLayer.IdentityDtos.UserDto;

namespace manav.webUI.Services.UserIdentityServices;

public interface IUserIdentityService
{
    Task<List<ResultUserListDto>> GetAllUserAsync();
    Task<ResultUserListDto> GetUserInfo(string id);

}