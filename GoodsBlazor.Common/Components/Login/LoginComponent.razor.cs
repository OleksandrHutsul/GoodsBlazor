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
            Navigation.NavigateTo("/products", true);
        }
        else
        {
            errorMessage = "Невірний email або пароль";
        }
    }
}
