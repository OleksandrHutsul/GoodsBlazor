using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using GoodsBlazor.Shared.Dtos;
using GoodsBlazor.Shared.Models;

namespace GoodsBlazor.Common.Components.ImageUploader;

public partial class ImageUploaderComponent
{
    [Parameter] public ProductDto Product { get; set; } = new();
    [Inject] private IJSRuntime JS { get; set; } = default!;

    private string? PreviewImageUrl { get; set; }

    private async Task OpenFilePicker()
    {
        var result = await JS.InvokeAsync<ImageUploadResult>("openFilePicker");

        if (result is not null)
        {
            PreviewImageUrl = result.ObjectUrl;

            if (result.ByteArray?.Count > 0)
                Product.ImageBase64 = Convert.ToBase64String(result.ByteArray.ToArray());

            StateHasChanged();
        }
    }

    private void ClearImage()
    {
        PreviewImageUrl = null;
        Product.ImageBase64 = null;
    }
}