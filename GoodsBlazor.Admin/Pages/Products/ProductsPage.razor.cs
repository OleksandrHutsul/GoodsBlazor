using GoodsBlazor.Admin.Services.Interfaces;
using GoodsBlazor.Shared.Dtos;
using Microsoft.AspNetCore.Components;

namespace GoodsBlazor.Admin.Pages.Products;

public partial class ProductsPage
{
    private List<ProductDto>? ProductList;

    [Inject] public required IProductService ProductService { get; set; }
    [Inject] public required NavigationManager NavigationManager { get; set; }

    protected override async Task OnInitializedAsync()
    {
        ProductList = await ProductService.GetAllProductsAsync();
    }

    private void EditProduct(int id)
    {
        NavigationManager.NavigateTo($"/edit-product/{id}");
    }

    private async Task DeleteProduct(int id)
    {
        var response = await ProductService.DeleteProductAsync(id);

        if (response.IsSuccessStatusCode)
        {
            ProductList = ProductList?.Where(p => p.Id != id).ToList();
            StateHasChanged();
        }
    }
}
