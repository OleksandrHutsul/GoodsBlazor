using MediatR;

namespace GoodsBlazor.BLL.Services.CartItemRepository.Commands.AddToCart;

public class AddToCartCommand : IRequest
{
    public int UserId { get; set; }
    public int ProductId { get; set; }
}