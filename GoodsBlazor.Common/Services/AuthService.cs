using System.Net.Http.Json;
using GoodsBlazor.Common.Services.Auth;
using GoodsBlazor.Shared.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace GoodsBlazor.Common.Services;

public class AuthService
{
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _jsRuntime;
    private readonly AuthenticationStateProvider _authStateProvider;

    public AuthService(HttpClient httpClient, IJSRuntime jsRuntime, AuthenticationStateProvider authStateProvider)
    {
        _httpClient = httpClient;
        _jsRuntime = jsRuntime;
        _authStateProvider = authStateProvider;
    }

    public async Task<bool> LoginAsync(string email, string password)
    {
        var response = await _httpClient.PostAsJsonAsync("api/users/login", new { Email = email, Password = password });

        if (!response.IsSuccessStatusCode) return false;

        var authResult = await response.Content.ReadFromJsonAsync<AuthResultDto>();
        if (authResult is null) return false;

        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "accessToken", authResult.AccessToken);

        await ((CustomAuthStateProvider)_authStateProvider).NotifyUserAuthentication(authResult.AccessToken);

        return true;
    }

    public async Task LogoutAsync()
    {
        var response = await _httpClient.PostAsync("api/users/logout", null);

        if (response.IsSuccessStatusCode)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "accessToken");
            await ((CustomAuthStateProvider)_authStateProvider).NotifyUserLogout();
        }
        else
        {
            Console.WriteLine($"Logout failed: {response.StatusCode}");
        }
    }
}
