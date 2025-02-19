using GoodsBlazor.BLL.Services.User.Dtos;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace GoodsBlazor.Client.Cookie;

public class CookieAuthStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient;

    public CookieAuthStateProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var response = await _httpClient.GetAsync("api/cookie/user");
        if (!response.IsSuccessStatusCode)
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        var user = await response.Content.ReadFromJsonAsync<UserDto>();
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        return new AuthenticationState(new ClaimsPrincipal(identity));
    }
}