using GoodsBlazor.DAL.Entities;
using MediatR;

namespace GoodsBlazor.BLL.Services.User.Commands.Register;

public class RegisterCommand: IRequest<int>
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public Role Role { get; set; } = Role.User;
}
