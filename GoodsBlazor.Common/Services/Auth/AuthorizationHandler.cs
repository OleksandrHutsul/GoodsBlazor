using System.Net.Http.Headers;
using Blazored.LocalStorage;

namespace GoodsBlazor.Common.Services.Auth;

public class TokenHandler : DelegatingHandler
{
    private readonly ILocalStorageService _localStorage;

    public TokenHandler(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _localStorage.GetItemAsync<string>("accessToken");

        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}

