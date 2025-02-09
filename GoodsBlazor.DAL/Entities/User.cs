namespace GoodsBlazor.DAL.Entities;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public Role Role { get; set; } = Role.User;
}