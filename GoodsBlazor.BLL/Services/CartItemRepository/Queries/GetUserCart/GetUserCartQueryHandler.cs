using AutoMapper;
using GoodsBlazor.BLL.Interfaces;
using GoodsBlazor.BLL.Services.CartItemRepository.Dtos;
using MediatR;

namespace GoodsBlazor.BLL.Services.CartItemRepository.Queries.GetUserCart;

public class GetUserCartQueryHandler(ICartItemRepository cartRepository, IMapper mapper)
    : IRequestHandler<GetUserCartQuery, List<CartItemDto>>
{
    public async Task<List<CartItemDto>> Handle(GetUserCartQuery request, CancellationToken cancellationToken)
    {
        var cartItems = await cartRepository.GetUserCart(request.UserId);
        return mapper.Map<List<CartItemDto>>(cartItems);
    }
}