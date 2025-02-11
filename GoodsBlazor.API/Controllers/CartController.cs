using GoodsBlazor.BLL.Services.CartItemRepository.Commands.AddToCart;
using GoodsBlazor.BLL.Services.CartItemRepository.Commands.RemoveFromCart;
using GoodsBlazor.BLL.Services.CartItemRepository.Dtos;
using GoodsBlazor.BLL.Services.CartItemRepository.Queries.GetUserCart;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GoodsBlazor.API.Controllers;

[ApiController]
[Route("api/cart")]
[Authorize]
public class CartController(IMediator mediator) : ControllerBase
{
    [HttpPost("add")]
    [Authorize]
    public async Task<IActionResult> AddToCart([FromBody] AddToCartCommand command)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        command.UserId = userId;

        await mediator.Send(command);
        return Ok();
    }

    [HttpPost("remove")]
    [Authorize]
    public async Task<IActionResult> RemoveFromCart([FromBody] RemoveFromCartCommand command)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        command.UserId = userId;

        await mediator.Send(command);
        return Ok();
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<List<CartItemDto>>> GetUserCart()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await mediator.Send(new GetUserCartQuery { UserId = userId });
        return Ok(result);
    }
}