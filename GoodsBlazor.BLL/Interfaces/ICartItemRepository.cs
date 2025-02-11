using GoodsBlazor.DAL.Entities;

namespace GoodsBlazor.BLL.Interfaces;

public interface ICartItemRepository
{
    public Task AddToCart(int userId, int productId);
    public Task<List<CartItem>> GetUserCart(int userId);
    public Task RemoveFromCart(int userId, int productId);
}
