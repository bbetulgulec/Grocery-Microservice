using manav.webUI.Models;

namespace manav.webUI.Services.Interfaces;

public interface IUserService
{
    Task<UserDetailViewModel> GetUser();
}