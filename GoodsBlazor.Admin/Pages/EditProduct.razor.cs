using GoodsBlazor.Shared.Dtos;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace GoodsBlazor.Admin.Pages;

public partial class EditProduct
{
    [Parameter] public int Id { get; set; }
    private ProductDto? Product { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Product = await Http.GetFromJsonAsync<ProductDto>($"api/products/{Id}");
    }

    private async Task UpdateProduct()
    {
        var response = await Http.PatchAsJsonAsync($"api/products/{Id}", Product);

        if (response.IsSuccessStatusCode)
        {
            Navigation.NavigateTo("/");
        }
        else
        {
            Console.WriteLine($"Помилка оновлення: {response.StatusCode}");
        }
    }

    private void Cancel()
    {
        Navigation.NavigateTo("/");
    }
}
