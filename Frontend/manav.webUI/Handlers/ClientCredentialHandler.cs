using System.Net.Http.Headers;
using manav.webUI.Services.Interfaces;

namespace manav.webUI.Handlers;

public class ClientCredentialHandler : DelegatingHandler
{
    private readonly IClientCredentialTokenService _tokenService;

    public ClientCredentialHandler(IClientCredentialTokenService tokenService)
    {
        _tokenService = tokenService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _tokenService.GetToken());
        var response = await base.SendAsync(request, cancellationToken);
        return response;
    }
}