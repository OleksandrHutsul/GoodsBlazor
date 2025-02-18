using System.Net.Http.Json;
using GoodsBlazor.Admin.Services.Interfaces;
using GoodsBlazor.Shared.Dtos;

namespace GoodsBlazor.Admin.Services;

public class ProductService : IProductService
{
    private readonly HttpClient _http;

    public ProductService(HttpClient http)
    {
        _http = http;
    }

    public Task<ProductDto?> GetProductByIdAsync(int id)
    {
        return _http.GetFromJsonAsync<ProductDto>($"api/products/{id}");
    }
    
    public Task UpdateProductAsync(int id, ProductDto product)
    {
        return _http.PatchAsJsonAsync($"api/products/{id}", product);
    }

    public Task CreateProductAsync(ProductDto product)
    {
        return _http.PostAsJsonAsync("api/products", product);
    }

    public Task<List<ProductDto>> GetAllProductsAsync()
    {
        return _http.GetFromJsonAsync<List<ProductDto>>("api/products");
    }

    public Task<HttpResponseMessage> DeleteProductAsync(int id)
    {
        return _http.DeleteAsync($"api/products/{id}");
    }

}