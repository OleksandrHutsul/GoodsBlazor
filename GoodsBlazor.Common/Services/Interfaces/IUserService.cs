using GoodsBlazor.Common.Components.Models;

namespace GoodsBlazor.Common.Services.Interfaces;

public interface IUserService
{
    Task<(bool IsSuccess, string? ErrorMessage)> RegisterUserAsync(RegisterRequest request);
    Task<bool> LoginAsync(string email, string password);
}
