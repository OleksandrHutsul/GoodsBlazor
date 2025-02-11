using GoodsBlazor.BLL.Interfaces;
using GoodsBlazor.DAL.Context;
using GoodsBlazor.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoodsBlazor.BLL.Repositories;

public class CartItemRepository(GoodsDbContext dbContext) : ICartItemRepository
{
    public async Task AddToCart(int userId, int productId)
    {
        var cartItem = new CartItem { UserId = userId, ProductId = productId };
        await dbContext.CartItems.AddAsync(cartItem);
        await dbContext.SaveChangesAsync();
    }

    public async Task<List<CartItem>> GetUserCart(int userId)
    {
        return await dbContext.CartItems
            .Include(ci => ci.Product)
            .Where(ci => ci.UserId == userId)
            .ToListAsync();
    }

    public async Task RemoveFromCart(int userId, int productId)
    {
        var item = await dbContext.CartItems.FirstOrDefaultAsync(ci => ci.UserId == userId && ci.ProductId == productId);
        if (item != null)
        {
            dbContext.CartItems.Remove(item);
            await dbContext.SaveChangesAsync();
        }
    }
}
