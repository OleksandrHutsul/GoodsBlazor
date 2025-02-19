using GoodsBlazor.Shared.Dtos;

namespace GoodsBlazor.Admin.Services.Interfaces;

public interface IProductService
{
    Task<ProductDto?> GetProductByIdAsync(int id);
    Task UpdateProductAsync(int id, ProductDto product);
    Task CreateProductAsync(ProductDto product);
    Task<List<ProductDto>> GetAllProductsAsync();
    Task<HttpResponseMessage> DeleteProductAsync(int id);
    Task<IEnumerable<ProductTypeDto>> GetAllProductTypesAsync();
}