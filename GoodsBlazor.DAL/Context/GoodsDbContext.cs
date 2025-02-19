using GoodsBlazor.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoodsBlazor.DAL.Context;

public class GoodsDbContext(DbContextOptions<GoodsDbContext> options): DbContext(options)
{
    internal DbSet<CartItem> CartItems { get; set; }
    internal DbSet<Product> Products { get; set; }
    internal DbSet<User> Users { get; set; }
    internal DbSet<ProductType> ProductsType { get; set; }
}
