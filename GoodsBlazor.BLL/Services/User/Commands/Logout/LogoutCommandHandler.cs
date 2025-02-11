using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace GoodsBlazor.BLL.Services.User.Commands.Logout;

public class LogoutCommandHandler(IHttpContextAccessor httpContextAccessor) : IRequestHandler<LogoutCommand>
{
    public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var context = httpContextAccessor.HttpContext!;

        context.Response.Cookies.Delete("RefreshToken");
        context.Response.Headers.Add("Clear-Access-Token", "true");
    }
}
