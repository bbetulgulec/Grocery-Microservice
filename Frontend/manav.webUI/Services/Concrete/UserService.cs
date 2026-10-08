using manav.webUI.Models;
using manav.webUI.Services.Interfaces;

namespace manav.webUI.Services.Concrete;

public class UserService:IUserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UserDetailViewModel> GetUser()
    {
        return await _httpClient.GetFromJsonAsync<UserDetailViewModel>("/api/user/getuser");
        
    }
}