using manav.DtoLayer.IdentityDtos.UserDto;

namespace manav.webUI.Services.UserIdentityServices;

public class UserIdentityService:IUserIdentityService
{
    private readonly HttpClient _httpClient;

    public UserIdentityService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<List<ResultUserListDto>> GetAllUserAsync()
    {
        var responseMassage =   await _httpClient.GetAsync($"user/getalluserlist");
        var values = await responseMassage.Content.ReadFromJsonAsync<List<ResultUserListDto>>();
        return values;
        
    }

    public async Task<ResultUserListDto> GetUserInfo(string id)
    {
        var responseMassage = await _httpClient.GetAsync($"user/getuserinfo/{id}");
        if (!responseMassage.IsSuccessStatusCode)
        { 
            return null;
        }
        var content = await responseMassage.Content.ReadAsStringAsync();
        if (string.IsNullOrWhiteSpace(content))
        {
            return null;
        }
        var values = await responseMassage.Content.ReadFromJsonAsync<ResultUserListDto>();
        return values;
    }
}