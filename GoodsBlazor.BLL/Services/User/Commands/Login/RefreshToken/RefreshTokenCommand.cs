using GoodsBlazor.Shared.Models;
using MediatR;

namespace GoodsBlazor.BLL.Services.User.Commands.Login.RefreshToken;

public class RefreshTokenCommand : IRequest<AuthResultDto>
{
}
