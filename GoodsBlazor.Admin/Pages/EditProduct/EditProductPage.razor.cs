using GoodsBlazor.Shared.Dtos;
using Microsoft.AspNetCore.Components;
using GoodsBlazor.Admin.Services.Interfaces;

namespace GoodsBlazor.Admin.Pages.EditProduct;

public partial class EditProductPage
{
    [Parameter] public int? Id { get; set; }

    [Inject] public required IProductService productService { get; set; }
    [Inject] public required NavigationManager NavigationManager { get; set; }

    private ProductDto? _product;
    private List<ProductTypeDto> _productTypes = new();

    protected override async Task OnInitializedAsync()
    {
        if (Id is null)
        {
            BackToProductPage();
            return;
        }

        _product = await productService.GetProductByIdAsync(Id.Value);
        _productTypes = (await productService.GetAllProductTypesAsync()).ToList();
    }

    private async Task SaveChangesAsync()
    {
        if (_product is null)
            return;
        
        Console.WriteLine($"ImageBase64 Length: {_product.ImageBase64?.Length}");
        await productService.UpdateProductAsync(Id.Value, _product);
        BackToProductPage();
    }

    private Task OnProductTypeSelected(ProductTypeDto selectedType)
    {
        _product.ProductTypeId = selectedType.Id;
        return Task.CompletedTask;
    }

    private void BackToProductPage() => NavigationManager.NavigateTo("/products");
}