using GoodsBlazor.Shared.Dtos;
using Microsoft.AspNetCore.Components;

namespace GoodsBlazor.Common.Components.ProductList;

public partial class ProductListComponents
{
    [Parameter] public List<ProductDto> Products { get; set; } = new();
    [Parameter] public EventCallback<int> OnEdit { get; set; }
    [Parameter] public EventCallback<int> OnDelete { get; set; }
    [Parameter] public EventCallback<int> OnAddToCart { get; set; }
    [Parameter] public bool IsAdmin { get; set; }

    private ProductDto? SelectedProduct { get; set; }

    private async Task DeleteProductAsync(int productId)
    {
        Console.WriteLine($"Deleting product with ID: {productId}");
        await OnDelete.InvokeAsync(productId);
    }

    private void ShowProductDetails(ProductDto product)
    {
        SelectedProduct = product;
    }

    private void CloseModal()
    {
        SelectedProduct = null;
    }
}
