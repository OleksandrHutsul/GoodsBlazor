using GoodsBlazor.Shared.Models;
using MediatR;

namespace GoodsBlazor.BLL.Services.User.Commands.Login;

public class LoginCommand : IRequest<AuthResultDto>
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}