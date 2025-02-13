using Blazored.LocalStorage;

namespace GoodsBlazor.Common.Services.GetToken;

public class TokenProvider
{
    private readonly ILocalStorageService _localStorage;

    public TokenProvider(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task<string?> GetTokenAsync()
    {
        return await _localStorage.GetItemAsync<string>("accessToken");
    }
}