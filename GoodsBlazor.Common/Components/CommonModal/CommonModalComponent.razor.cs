using Microsoft.AspNetCore.Components;

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

    [Parameter, EditorRequired]
    public required string Message { get; set; }

    [Parameter] public EventCallback<bool> IsOpenChanged { get; set; }

    private async Task CloseModal()
    {
        IsOpen = false;
    }
}
