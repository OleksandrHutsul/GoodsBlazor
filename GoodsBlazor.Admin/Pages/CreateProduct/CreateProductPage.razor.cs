using GoodsBlazor.Shared.Dtos;
using Microsoft.AspNetCore.Components;
using GoodsBlazor.Admin.Services.Interfaces;

namespace GoodsBlazor.Admin.Pages.CreateProduct;

public partial class CreateProductPage
{
    [Inject] public required IProductService ProductService { get; set; }
    [Inject] public required NavigationManager NavigationManager { get; set; }
    
    private ProductDto _product = new();
    private List<ProductTypeDto> _productTypes = new();
    
    protected override async Task OnInitializedAsync()
    {
        var categories = await ProductService.GetAllProductTypesAsync();
        _productTypes = categories.ToList();
    }

    private async Task AdminCreateProductAsync()
    {
        if (!string.IsNullOrEmpty(_product.Name) && _product.Price > 0 && _product.ProductTypeId > 0)
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

    private void OnProductTypeSelected(ProductTypeDto selectedType)
    {
        _product.ProductTypeId = selectedType.Id;
    }
}