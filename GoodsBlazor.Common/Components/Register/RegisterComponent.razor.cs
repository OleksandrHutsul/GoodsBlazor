using Microsoft.AspNetCore.Components;
using GoodsBlazor.DAL.Entities;
using GoodsBlazor.Common.Components.Models;
using GoodsBlazor.Common.Services.Interfaces;

namespace GoodsBlazor.Common.Components.Register;

public partial class RegisterComponent
{
    [Parameter] public Role Role { get; set; } = Role.User;

    [Inject] public required IUserService UserService { get; set; }
    [Inject] public required NavigationManager NavigationManager { get; set; }
    
    private string _email = "";
    private string _password = "";
    private string? _errorMessage;
    private string? _successMessage;
    private bool _isLoading = false;    

    private async Task RegisterAsync()
    {
        _errorMessage = null;
        _successMessage = null;

        if (string.IsNullOrWhiteSpace(_email) || string.IsNullOrWhiteSpace(_password))
        {
            _errorMessage = "All fields must be filled in.";
            return;
        }

        _isLoading = true;

        try
        {
            var request = new RegisterRequest
            {
                Email = _email,
                Password = _password,
                Role = Role
            };

            var (isSuccess, error) = await UserService.RegisterUserAsync(request);

            if (isSuccess)
            {
                _successMessage = "Registration successful! You can log in.";
                _email = "";
                _password = "";
            }
            else
            {
                _errorMessage = error;
            }
        }
        catch (Exception ex)
        {
            _errorMessage = $"Error: {ex.Message}";
        }
        finally
        {
            _isLoading = false;
        }
    }
}
