using GoodsBlazor.BLL.Services.User.Commands.Login.RefreshToken;
using GoodsBlazor.BLL.Services.User.Commands.Login;
using GoodsBlazor.BLL.Services.User.Commands.Logout;
using GoodsBlazor.BLL.Services.User.Commands.Register;
using GoodsBlazor.BLL.Services.User.Dtos;
using GoodsBlazor.BLL.Services.User.Queries.GetUserById;
using GoodsBlazor.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace GoodsBlazor.API.Controllers;

[ApiController]
[Route("api/users")]
public class UserController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateUser(RegisterCommand command)
    {
        int id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto?>> GetById([FromRoute] int id)
    {
        var user = await mediator.Send(new GetUserByIdQuery(id));
        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResultDto>> Login([FromBody] LoginCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("refresh")]
    [Authorize]
    public async Task<ActionResult<AuthResultDto>> RefreshToken()
    {
        var result = await mediator.Send(new RefreshTokenCommand());
        return Ok(result);
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await mediator.Send(new LogoutCommand());
        Response.Headers.Add("Clear-Access-Token", "true");
        return NoContent();
    }
}
