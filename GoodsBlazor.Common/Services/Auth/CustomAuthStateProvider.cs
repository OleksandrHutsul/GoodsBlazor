using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.JSInterop;
using System.Security.Claims;

namespace GoodsBlazor.Common.Services.Auth;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly IJSRuntime _jsRuntime;
    private ClaimsPrincipal _currentUser = new(new ClaimsIdentity());

    public CustomAuthStateProvider(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "accessToken");

        if (string.IsNullOrEmpty(token))
        {
            return new AuthenticationState(_currentUser);
        }

        var identity = ParseClaimsFromJwt(token);
        _currentUser = new ClaimsPrincipal(identity);

        return new AuthenticationState(_currentUser);
    }

    public async Task NotifyUserAuthenticationAsync(string token)
    {
        var identity = ParseClaimsFromJwt(token);
        _currentUser = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
    }

    public async Task NotifyUserLogoutAsync()
    {
        _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
    }

    private static ClaimsIdentity ParseClaimsFromJwt(string token)
    {
        var handler = new JsonWebTokenHandler();
        var jsonToken = handler.ReadJsonWebToken(token);

        var claims = jsonToken.Claims.Select(c => new Claim(c.Type, c.Value));
        return new ClaimsIdentity(claims, "jwt");
    }
}
