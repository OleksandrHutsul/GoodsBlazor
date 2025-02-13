using GoodsBlazor.DAL.Entities;

namespace GoodsBlazor.Common.Components.Models;

public class RegisterRequest
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public Role Role { get; set; }
}
