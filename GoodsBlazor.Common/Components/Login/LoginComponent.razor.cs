using GoodsBlazor.Common.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace GoodsBlazor.Common.Components.Login;

public partial class LoginComponent
{
    [Inject] public required IUserService UserService { get; set; }
    [Inject] public required NavigationManager NavigationManager { get; set; }

    private string _email = "";
    private string _password = "";
    private string? _errorMessage;

    private async Task Login()
    {
        var success = await UserService.LoginAsync(_email, _password);
        if (success)
            NavigationManager.NavigateTo("/products");
        else
            _errorMessage = "Incorrect email or password";
    }
}
