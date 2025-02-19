using GoodsBlazor.DAL.Entities;

namespace GoodsBlazor.BLL.Interfaces;

public interface IProductRepository
{
    public Task<int> Create(Product entity);
    public Task Delete(Product entity);
    public Task<IEnumerable<Product>> GetAllAsync();
    public Task<Product?> GetByIdAsync(int id);
    public Task SaveChanges();
    Task<IEnumerable<ProductType>> GetAllProductTypeAsync();
}