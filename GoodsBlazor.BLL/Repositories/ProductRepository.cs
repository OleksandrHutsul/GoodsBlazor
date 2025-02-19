using GoodsBlazor.BLL.Interfaces;
using GoodsBlazor.DAL.Context;
using GoodsBlazor.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoodsBlazor.BLL.Repositories;

public class ProductRepository(GoodsDbContext dbContext): IProductRepository
{
    public async Task<int> Create(Product entity)
    {
        dbContext.Products.Add(entity);
        await dbContext.SaveChangesAsync();

        return entity.Id;
    }

    public async Task Delete(Product entity)
    {
        dbContext.Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        var products = await dbContext.Products
            .ToListAsync();

        return products;
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        var products = await dbContext.Products
            .Include(p => p.ProductType)
            .FirstOrDefaultAsync(x => x.Id == id);
        return products;
    }

    public async Task<IEnumerable<ProductType>> GetAllProductTypeAsync()
    {
        var products = await dbContext.ProductsType
            .ToListAsync();

        return products;
    }

    public Task SaveChanges()
        => dbContext.SaveChangesAsync();
}