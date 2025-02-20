using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using GoodsBlazor.Common.Services.Auth;
using GoodsBlazor.Common.Components.Models;

namespace GoodsBlazor.Common.Components.NavMenu;

public partial class NavMenuComponent
{
    [Inject] public required AuthenticationStateProvider AuthStateProvider { get; set; }
    [Inject] public required CustomAuthStateProvider CustomAuthStateProvider { get; set; }
    [Inject] public required IJSRuntime JSRuntime { get; set; }
    [Inject] public required NavigationManager NavigationManager { get; set; }

    [Parameter] public string BrandName { get; set; } = "Brand";
    [Parameter] public string BrandUrl { get; set; } = "/";
    [Parameter] public List<NavMenuItem> MenuItems { get; set; } = new();

    private bool _isAuthenticated;

    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthStateProvider.GetAuthenticationStateAsync();
        _isAuthenticated = authState.User.Identity?.IsAuthenticated == true;
    }

    private async Task LogoutAsync()
    {
        await JSRuntime.InvokeVoidAsync("localStorage.removeItem", "accessToken");
        await CustomAuthStateProvider.NotifyUserLogoutAsync();
        NavigationManager.NavigateTo("/", forceLoad: true);
    }
}