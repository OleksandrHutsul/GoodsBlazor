using GoodsBlazor.BLL.Interfaces;
using MediatR;

namespace GoodsBlazor.BLL.Services.CartItemRepository.Commands.AddToCart;

public class AddToCartCommandHandler(ICartItemRepository cartRepository) : IRequestHandler<AddToCartCommand>
{
    public async Task Handle(AddToCartCommand request, CancellationToken cancellationToken)
    {
        await cartRepository.AddToCart(request.UserId, request.ProductId);
    }
}