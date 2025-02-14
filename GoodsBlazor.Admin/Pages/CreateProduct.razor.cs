using GoodsBlazor.Shared.Dtos;
using System.Net.Http.Json;

namespace GoodsBlazor.Admin.Pages;

public partial class CreateProduct
{
    private ProductDto Product { get; set; } = new();

    private async Task AdminCreateProduct()
    {
        if (!string.IsNullOrEmpty(Product.Name) && Product.Price > 0)
        {
            Console.WriteLine($"ImageBase64 Length: {Product.ImageBase64?.Length}");
            await Http.PostAsJsonAsync("api/products", Product);
            Navigation.NavigateTo("/products");
        }
        else
        {
            Console.WriteLine("Invalid product data");
        }
    }

    private void Cancel()
    {
        Navigation.NavigateTo("/products");
    }
}