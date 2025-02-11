using GoodsBlazor.BLL.Services.CartItemRepository.Dtos;
using MediatR;

namespace GoodsBlazor.BLL.Services.CartItemRepository.Queries.GetUserCart;

public class GetUserCartQuery : IRequest<List<CartItemDto>>
{
    public int UserId { get; set; }
}