using GoodsBlazor.Common.Components.Models;
using GoodsBlazor.Common.Services.Auth;
using GoodsBlazor.Common.Services.Interfaces;
using GoodsBlazor.Shared.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace GoodsBlazor.Common.Services.Register;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _jsRuntime;
    private readonly AuthenticationStateProvider _authStateProvider;

    public UserService(HttpClient httpClient, IJSRuntime jsRuntime, AuthenticationStateProvider authStateProvider)
    {
        _httpClient = httpClient;
        _jsRuntime = jsRuntime;
        _authStateProvider = authStateProvider;
    }

    public async Task<(bool IsSuccess, string? ErrorMessage)> RegisterUserAsync(RegisterRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/users", request);

            if (response.IsSuccessStatusCode)
            {
                return (true, null);
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                return (false, $"Error: {errorResponse}");
            }
        }
        catch (Exception ex)
        {
            return (false, $"Error: {ex.Message}");
        }
    }

    public async Task<bool> LoginAsync(string email, string password)
    {
        var response = await _httpClient.PostAsJsonAsync("api/users/login", new { Email = email, Password = password });

        if (!response.IsSuccessStatusCode) return false;

        var authResult = await response.Content.ReadFromJsonAsync<AuthResultDto>();

        if (authResult is null) return false;

        if (!string.IsNullOrEmpty(authResult.AccessToken))
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "accessToken", authResult.AccessToken);
            await ((CustomAuthStateProvider)_authStateProvider).NotifyUserAuthenticationAsync(authResult.AccessToken);
        }
        else
        {
            await ((CustomAuthStateProvider)_authStateProvider).NotifyUserAuthenticationAsync("");
        }

        return true;
    }

}