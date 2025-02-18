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
    private bool IsModalOpen { get; set; }

    protected override void OnParametersSet()
    {
        if (!string.IsNullOrEmpty(Product.ImageBase64))
            PreviewImageUrl = $"data:image/png;base64,{Product.ImageBase64}";
    }

    private async Task OpenFilePicker()
    {
        var result = await JS.InvokeAsync<ImageUploadResult>("openFilePicker");

        if (result is not null)
        {
            PreviewImageUrl = result.ObjectUrl;
            if (result.ByteArray?.Count > 0)
                Product.ImageBase64 = Convert.ToBase64String(result.ByteArray.ToArray());

            IsModalOpen = true;
            StateHasChanged();
        }
    }

    private void ClearImage()
    {
        PreviewImageUrl = null;
        Product.ImageBase64 = null;
        IsModalOpen = false;
    }

    private void OnModalStateChanged(bool isOpen)
    {
        IsModalOpen = isOpen;
    }
}