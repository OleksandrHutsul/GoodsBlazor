using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using GoodsBlazor.Shared.Dtos;

namespace GoodsBlazor.Common.Components.ImageUploader;

public partial class ImageUploaderComponent
{
    [Parameter] public ProductDto Product { get; set; } = new();
    [Inject] private IJSRuntime JS { get; set; } = default!;

    private string? PreviewImageUrl =>
        !string.IsNullOrEmpty(Product?.ImageBase64)
            ? $"data:image/png;base64,{Product.ImageBase64}"
            : null;

    private async Task OpenFilePicker()
    {
        var base64String = await JS.InvokeAsync<string>("openFilePicker");
        if (!string.IsNullOrEmpty(base64String))
        {
            Product.ImageBase64 = base64String;
            await InvokeAsync(StateHasChanged);
        }
    }

    private void ClearImage()
    {
        Product.ImageBase64 = null;
    }
}