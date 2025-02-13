using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using GoodsBlazor.DAL.Entities;
using GoodsBlazor.Common.Components.Models;

namespace GoodsBlazor.Common.Components.Register;

public partial class RegisterComponent
{
    [Parameter] public Role Role { get; set; } = Role.User;

    private string email = "";
    private string password = "";
    private string? errorMessage;
    private string? successMessage;
    private bool isLoading = false;

    [Inject] private HttpClient Http { get; set; } = default!;
    [Inject] private NavigationManager Navigation { get; set; } = default!;

    private async Task Register()
    {
        errorMessage = null;
        successMessage = null;

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            errorMessage = "Усі поля повинні бути заповнені.";
            return;
        }

        isLoading = true;

        try
        {
            var request = new RegisterRequest
            {
                Email = email,
                Password = password,
                Role = Role
            };

            var response = await Http.PostAsJsonAsync("api/users", request);

            if (response.IsSuccessStatusCode)
            {
                successMessage = "Реєстрація успішна! Ви можете увійти.";
                email = "";
                password = "";
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                errorMessage = $"Помилка: {errorResponse}";
            }
        }
        catch (Exception ex)
        {
            errorMessage = $"Помилка: {ex.Message}";
        }
        finally
        {
            isLoading = false;
        }
    }
}
