using GoodsBlazor.Admin.Services.Interfaces;
using GoodsBlazor.Shared.Dtos;
using Microsoft.AspNetCore.Components;

namespace GoodsBlazor.Admin.Pages.Products;

public partial class ProductsPage
{
    [Inject] public required IProductService ProductService { get; set; }
    [Inject] public required NavigationManager NavigationManager { get; set; }

    public bool _isLoading = true;
    private List<ProductDto>? _productList;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            _productList = await ProductService.GetAllProductsAsync();
        }
        finally
        {
            _isLoading = false;
            StateHasChanged();
        }
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
            _productList = _productList?.Where(p => p.Id != id).ToList();
            StateHasChanged();
        }
    }
}
