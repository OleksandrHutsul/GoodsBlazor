namespace GoodsBlazor.Common.Components.Login;

public partial class LoginComponent
{
    private string email = "";
    private string password = "";
    private string? errorMessage;

    private async Task Login()
    {
        var success = await AuthService.LoginAsync(email, password);
        if (success)
        {
            Navigation.NavigateTo("/", true);
        }
        else
        {
            errorMessage = "Невірний email або пароль";
        }
    }
}
