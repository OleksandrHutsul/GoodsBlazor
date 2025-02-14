using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using GoodsBlazor.Shared.Dtos;

namespace GoodsBlazor.Common.Components.ImageUploader;

public partial class ImageUploaderComponent
{
    [Parameter] public ProductDto Product { get; set; }

    private string? PreviewImageUrl =>
        !string.IsNullOrEmpty(Product?.ImageBase64)
            ? $"data:image/png;base64,{Product.ImageBase64}"
            : null;

    private async Task OnImageSelected(InputFileChangeEventArgs e)
    {
        var file = e.File;
        if (file != null)
        {
            var buffer = new byte[file.Size];
            using var stream = file.OpenReadStream(maxAllowedSize: 1024 * 1024 * 5);
            await stream.ReadAsync(buffer);

            Product.ImageBase64 = Convert.ToBase64String(buffer);
            StateHasChanged();
        }
    }

    private void ClearImage()
    {
        Product.ImageBase64 = null;
    }
}