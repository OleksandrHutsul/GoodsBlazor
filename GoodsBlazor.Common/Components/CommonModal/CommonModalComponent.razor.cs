using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace GoodsBlazor.Common.Components.CommonModal;

public partial class CommonModalComponent
{
    private bool _isOpen;

    [Parameter]
    public bool IsOpen
    {
        get => _isOpen;
        set
        {
            if (_isOpen == value) return;
            _isOpen = value;
            IsOpenChanged.InvokeAsync(value);
        }
    }

    [Parameter] public string? ImageUrl { get; set; }
    [Parameter] public EventCallback<bool> IsOpenChanged { get; set; }
    [Parameter] public EventCallback DeleteImageCallback { get; set; } 

    [Inject] public required IJSRuntime JSRuntime { get; set; }

    private async Task CloseModalAsync() => IsOpen = false;

    private async Task OpenInNewTabAsync()
    {
        if (!string.IsNullOrEmpty(ImageUrl))
            await JSRuntime.InvokeVoidAsync("window.open", ImageUrl, "_blank");
    }

    private async Task DeleteImageAsync()
    {
        await DeleteImageCallback.InvokeAsync(); 
        CloseModalAsync();
    }
}