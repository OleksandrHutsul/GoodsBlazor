using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using GoodsBlazor.Common.Services.Auth;
using GoodsBlazor.Common.Components.Models;

namespace GoodsBlazor.Common.Components.NavMenu;

public partial class NavMenuComponent
{
    [Inject] private AuthenticationStateProvider AuthStateProvider { get; set; } = default!;
    [Inject] private CustomAuthStateProvider CustomAuthStateProvider { get; set; } = default!;
    [Inject] private IJSRuntime JSRuntime { get; set; } = default!;
    [Inject] private NavigationManager Navigation { get; set; } = default!;

    [Parameter] public string BrandName { get; set; } = "Brand";
    [Parameter] public string BrandUrl { get; set; } = "/";
    [Parameter] public List<NavMenuItem> MenuItems { get; set; } = new();

    private bool IsAuthenticated { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthStateProvider.GetAuthenticationStateAsync();
        IsAuthenticated = authState.User.Identity?.IsAuthenticated == true;
    }

    private async Task LogoutAsync()
    {
        await JSRuntime.InvokeVoidAsync("localStorage.removeItem", "accessToken");
        await CustomAuthStateProvider.NotifyUserLogout();
        Navigation.NavigateTo("/", forceLoad: true);
    }
}