using GoodsBlazor.Shared.Dtos;
using System.Net.Http.Json;

namespace GoodsBlazor.Admin.Pages.Products;

public partial class ProductsPage
{
    private List<ProductDto>? ProductList;

    protected override async Task OnInitializedAsync()
    {
        ProductList = await Http.GetFromJsonAsync<List<ProductDto>>("api/products");
    }

    private void EditProduct(int id)
    {
        Navigation.NavigateTo($"/edit-product/{id}");
    }

    private async Task DeleteProduct(int id)
    {
        var response = await Http.DeleteAsync($"api/products/{id}");
        if (response.IsSuccessStatusCode)
        {
            ProductList = ProductList?.Where(p => p.Id != id).ToList();
            StateHasChanged();
        }
    }
}
