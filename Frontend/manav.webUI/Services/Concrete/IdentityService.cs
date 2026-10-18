using System.Security.Claims;
using IdentityModel.Client;
using manav.DtoLayer.IdentityDtos.LoginDto;
using manav.DtoLayer.IdentityDtos.RegisterDto;
using manav.webUI.Services.Interfaces;
using manav.webUI.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace manav.webUI.Services.Concrete;

public class IdentityService : IIdentityService
{
    private readonly ClientSettings _clientSettings;
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ServiceApiSettings _serviceApiSettings;

    public IdentityService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor,
        IOptions<ClientSettings> clientSettings, IOptions<ServiceApiSettings> serviceApiSettings)
    {
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
        _clientSettings = clientSettings.Value;
        _serviceApiSettings = serviceApiSettings.Value;
    }

    public async Task<bool> SignIn(CreateLoginDto createLoginDto)
    {
        var discoveryEndPoint = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
        {
            Address = _serviceApiSettings.IdentityServerUrl,
            Policy = new DiscoveryPolicy
            {
                RequireHttps = false
            }
        });

        var passwordTokenRequest = new PasswordTokenRequest
        {
            ClientId = _clientSettings.manavManagerClient.ClientId,
            ClientSecret = _clientSettings.manavManagerClient.ClientSecret,
            UserName = createLoginDto.Email,
            Password = createLoginDto.Password,
            Address = discoveryEndPoint.TokenEndpoint
        };

        var token = await _httpClient.RequestPasswordTokenAsync(passwordTokenRequest);
        if (token.IsError || string.IsNullOrEmpty(token.AccessToken))
            return false;

        var userInfoRequest = new UserInfoRequest
        {
            Token = token.AccessToken,
            Address = discoveryEndPoint.UserInfoEndpoint
        };

        var userValues = await _httpClient.GetUserInfoAsync(userInfoRequest);

        if (userValues.IsError)
            return false;

        var claimsIdentity = new ClaimsIdentity(userValues.Claims,
            CookieAuthenticationDefaults.AuthenticationScheme, "name", "role");

        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        var authenticationProperties = new AuthenticationProperties();
        authenticationProperties.StoreTokens(new List<AuthenticationToken>
        {
            new()
            {
                Name = OpenIdConnectParameterNames.AccessToken,
                Value = token.AccessToken
            },
            new()
            {
                Name = OpenIdConnectParameterNames.RefreshToken,
                Value = token.RefreshToken
            },
            new()
            {
                Name = OpenIdConnectParameterNames.ExpiresIn,
                Value = DateTime.Now.AddSeconds(token.ExpiresIn).ToString()
            }
        });

        authenticationProperties.IsPersistent = false;
        await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            claimsPrincipal, authenticationProperties);
        return true;
    }

    public async Task<bool> SignInAsAdmin(CreateLoginDto createLoginDto)
    {
        var discoveryEndPoint = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
        {
            Address = _serviceApiSettings.IdentityServerUrl,
            Policy = new DiscoveryPolicy { RequireHttps = false }
        });

        var passwordTokenRequest = new PasswordTokenRequest
        {
            ClientId = _clientSettings.manavAdminClient.ClientId,
            ClientSecret = _clientSettings.manavAdminClient.ClientSecret,
            UserName = createLoginDto.Email,
            Password = createLoginDto.Password,
            Address = discoveryEndPoint.TokenEndpoint
        };

        var token = await _httpClient.RequestPasswordTokenAsync(passwordTokenRequest);

        if (token.IsError || string.IsNullOrEmpty(token.AccessToken))
        {
            Console.WriteLine("--- TOKEN ALMA HATASI ---");
            Console.WriteLine($"Error: {token.Error}");
            Console.WriteLine($"Error Description: {token.ErrorDescription}");
            Console.WriteLine($"HTTP Error: {token.HttpResponse?.ReasonPhrase}");
            return false;
        }

        var userInfoRequest = new UserInfoRequest
        {
            Token = token.AccessToken,
            Address = discoveryEndPoint.UserInfoEndpoint
        };

        var userValues = await _httpClient.GetUserInfoAsync(userInfoRequest);

        if (userValues.IsError)
        {
            Console.WriteLine($"UserInfo hatası: {userValues.Error}");
            return false;
        }

        // Sadece bu e-posta admin paneline girebilir
        const string adminEmail = "mevsimmanav@gmail.com";

        var emailClaim = userValues.Claims.FirstOrDefault(c => c.Type == "email")?.Value;

        if (!string.Equals(emailClaim, adminEmail, StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine($"Admin yetkisi yok: {emailClaim}");
            return false;
        }

        var claimsIdentity = new ClaimsIdentity(
            userValues.Claims,
            CookieAuthenticationDefaults.AuthenticationScheme,
            "name",
            "role"
        );
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        var authenticationProperties = new AuthenticationProperties();
        authenticationProperties.StoreTokens(new List<AuthenticationToken>
        {
            new() { Name = OpenIdConnectParameterNames.AccessToken, Value = token.AccessToken },
            new() { Name = OpenIdConnectParameterNames.RefreshToken, Value = token.RefreshToken },
            new()
            {
                Name = OpenIdConnectParameterNames.ExpiresIn,
                Value = DateTime.Now.AddSeconds(token.ExpiresIn).ToString()
            }
        });

        authenticationProperties.IsPersistent = false;
        await _httpContextAccessor.HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            claimsPrincipal,
            authenticationProperties
        );

        return true;
    }

    public async Task<bool> SignUp(CreateRegisterDto createRegisterDto)
    {
        if (createRegisterDto.Password != createRegisterDto.ConfimPassword)
            return false;

        var url = $"{_serviceApiSettings.IdentityServerUrl.TrimEnd('/')}/api/RegisterControllers";

        var response = await _httpClient.PostAsJsonAsync(url, createRegisterDto);
        var body = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Kayıt API hatası {(int)response.StatusCode}: {body}");
            return false;
        }

        return true;
    }

    public async Task<bool> GetRefreshToken()
    {
        var refreshToken =
            await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);

        if (string.IsNullOrEmpty(refreshToken)) return false;

        var discoveryEndPoint = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
        {
            Address = _serviceApiSettings.IdentityServerUrl,
            Policy = new DiscoveryPolicy
            {
                RequireHttps = false
            }
        });

        RefreshTokenRequest refreshTokenRequest = new()
        {
            ClientId = _clientSettings.manavManagerClient.ClientId,
            ClientSecret = _clientSettings.manavManagerClient.ClientSecret,
            Address = discoveryEndPoint.TokenEndpoint,
            RefreshToken = refreshToken
        };
        var token = await _httpClient.RequestRefreshTokenAsync(refreshTokenRequest);

        if (token.IsError || string.IsNullOrEmpty(token.AccessToken)) return false;

        var authanticationToken = new List<AuthenticationToken>
        {
            new()
            {
                Name = OpenIdConnectParameterNames.AccessToken,
                Value = token.AccessToken
            },
            new()
            {
                Name = OpenIdConnectParameterNames.RefreshToken,
                Value = token.RefreshToken
            },
            new()
            {
                Name = OpenIdConnectParameterNames.ExpiresIn,
                Value = DateTime.Now.AddSeconds(token.ExpiresIn).ToString()
            }
        };
        var result =
            await _httpContextAccessor.HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        var properties = result.Properties;
        properties.StoreTokens(authanticationToken);
        await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            result.Principal, properties);
        return true;
    }

    public async Task<bool> Logout()
    {
        if (_httpContextAccessor.HttpContext != null)
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return true;
        }

        return false;
    }
}