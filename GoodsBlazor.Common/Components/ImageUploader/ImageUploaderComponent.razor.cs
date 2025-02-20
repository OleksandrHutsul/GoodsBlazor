using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using GoodsBlazor.Shared.Dtos;
using GoodsBlazor.Shared.Models;

namespace GoodsBlazor.Common.Components.ImageUploader;

public partial class ImageUploaderComponent
{
    [Parameter] public ProductDto Product { get; set; } = new();
    [Inject] public required IJSRuntime JSRuntime { get; set; }

    private string? _previewImageUrl;
    private bool _isModalOpen;

    protected override void OnParametersSet()
    {
        if (!string.IsNullOrEmpty(Product.ImageBase64))
            _previewImageUrl = $"data:image/png;base64,{Product.ImageBase64}";
    }

    private async Task OpenFilePickerAsync()
    {
        var result = await JSRuntime.InvokeAsync<ImageUploadResult>("openFilePicker");

        if (result is not null)
        {
            _previewImageUrl = result.ObjectUrl;
            if (result.ByteArray?.Count > 0)
                Product.ImageBase64 = Convert.ToBase64String(result.ByteArray.ToArray());

            _isModalOpen = true;
            StateHasChanged();
        }
    }

    private void ClearImage()
    {
        _previewImageUrl = null;
        Product.ImageBase64 = null;
        _isModalOpen = false;
    }

    private void OnModalStateChanged(bool isOpen)
    {
        _isModalOpen = isOpen;
    }
}