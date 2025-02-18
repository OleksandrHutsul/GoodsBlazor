using GoodsBlazor.Shared.Dtos;
using Microsoft.AspNetCore.Components;
using GoodsBlazor.Admin.Services.Interfaces;

namespace GoodsBlazor.Admin.Pages.CreateProduct;

public partial class CreateProductPage
{
    private ProductDto _product = new();

    [Inject] public required IProductService ProductService { get; set; }
    [Inject] public required NavigationManager NavigationManager { get; set; }

    private async Task AdminCreateProduct()
    {
        if (!string.IsNullOrEmpty(_product.Name) && _product.Price > 0)
        {
            Console.WriteLine($"ImageBase64 Length: {_product.ImageBase64?.Length}");
            await ProductService.CreateProductAsync(_product);
            BackToProductPage();
        }
        else
        {
            Console.WriteLine("Invalid product data");
        }
    }

    private void BackToProductPage() => NavigationManager.NavigateTo("/products");
}