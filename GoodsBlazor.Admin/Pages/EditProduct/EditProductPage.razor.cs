using GoodsBlazor.Shared.Dtos;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace GoodsBlazor.Admin.Pages.EditProduct;

public partial class EditProductPage
{
    [Parameter] public int? Id { get; set; }
    private ProductDto? Product { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (Id == null)
        {
            Navigation.NavigateTo("/products");
            return;
        }

        Product = await Http.GetFromJsonAsync<ProductDto>($"api/products/{Id}");
    }

    private async Task SaveChanges()
    {
        if (Product != null)
        {
            Console.WriteLine($"ImageBase64 Length: {Product.ImageBase64?.Length}");
            await Http.PatchAsJsonAsync($"api/products/{Id}", Product);
            Navigation.NavigateTo("/products");
        }
    }

    private void Cancel()
    {
        Navigation.NavigateTo("/products");
    }
}
