using manav.webUI.Models;

namespace manav.webUI.Services.Interfaces;

public interface IClientCredentialTokenService
{
    Task<string> GetToken();
}