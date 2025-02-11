using MediatR;

namespace GoodsBlazor.BLL.Services.CartItemRepository.Commands.RemoveFromCart;

public class RemoveFromCartCommand : IRequest
{
    public int UserId { get; set; }
    public int ProductId { get; set; }
}