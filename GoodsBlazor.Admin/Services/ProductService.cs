using System.Net.Http.Json;
using GoodsBlazor.Admin.Services.Interfaces;
using GoodsBlazor.Shared.Dtos;

namespace GoodsBlazor.Admin.Services;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<ProductDto?> GetProductByIdAsync(int id)
    {
        return _httpClient.GetFromJsonAsync<ProductDto>($"api/products/{id}");
    }
    
    public Task UpdateProductAsync(int id, ProductDto product)
    {
        return _httpClient.PatchAsJsonAsync($"api/products/{id}", product);
    }

    public Task CreateProductAsync(ProductDto product)
    {
        return _httpClient.PostAsJsonAsync("api/products", product);
    }

    public Task<List<ProductDto>> GetAllProductsAsync()
    {
        return _httpClient.GetFromJsonAsync<List<ProductDto>>("api/products");
    }

    public Task<HttpResponseMessage> DeleteProductAsync(int id)
    {
        return _httpClient.DeleteAsync($"api/products/{id}");
    }

    public async Task<IEnumerable<ProductTypeDto>> GetAllProductTypesAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<ProductTypeDto>>("api/products/productstype") ?? new List<ProductTypeDto>();
    }
}