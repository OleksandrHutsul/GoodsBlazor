using GoodsBlazor.BLL.Interfaces;
using MediatR;

namespace GoodsBlazor.BLL.Services.CartItemRepository.Commands.RemoveFromCart;

public class RemoveFromCartCommandHandler(ICartItemRepository cartRepository) : IRequestHandler<RemoveFromCartCommand>
{
    public async Task Handle(RemoveFromCartCommand request, CancellationToken cancellationToken)
    {
        await cartRepository.RemoveFromCart(request.UserId, request.ProductId);
    }
}
