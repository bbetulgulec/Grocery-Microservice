using IdentityModel.Client;
using manav.webUI.Services.Interfaces;
using manav.webUI.Settings;
using Microsoft.Extensions.Options;
using IdentityModel.AspNetCore.AccessTokenManagement; 

namespace manav.webUI.Services.Concrete;

public class ClientCredentialTokenService:IClientCredentialTokenService
{
    private readonly ServiceApiSettings _serviceApiSettings;
    private readonly HttpClient _httpClient;
    private readonly IClientAccessTokenCache _clientAccessTokenCache;
    private readonly ClientSettings _clientSettings;

    public ClientCredentialTokenService(IOptions<ServiceApiSettings> serviceApiSettings, HttpClient httpClient, IClientAccessTokenCache clientAccessTokenCache, IOptions<ClientSettings> clientSettings)
    {
        _serviceApiSettings = serviceApiSettings.Value;
        _httpClient = httpClient;
        _clientAccessTokenCache = clientAccessTokenCache;
        _clientSettings = clientSettings.Value;
    }

    public async Task<string> GetToken()
    {
        var currentToken = await _clientAccessTokenCache.GetAsync("manavtoken");
        if (currentToken!=null)
        {
            return  currentToken.AccessToken;
        }
        var discoveryEndPoint = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
        {
            Address =_serviceApiSettings.IdentityServerUrl,
            Policy = new DiscoveryPolicy
            {
                RequireHttps = false,
            }
        });
        var clientCredentialTokenRequest = new ClientCredentialsTokenRequest
        {
            ClientId = _clientSettings.manavVisitorClient.ClientId,
            ClientSecret = _clientSettings.manavVisitorClient.ClientSecret,
            Address = discoveryEndPoint.TokenEndpoint
        };
        var newToken = await _httpClient.RequestClientCredentialsTokenAsync(clientCredentialTokenRequest);
        await _clientAccessTokenCache.SetAsync("manavtoken",newToken.AccessToken,newToken.ExpiresIn);
        return newToken.AccessToken;
    }
}