using Microsoft.AspNetCore.Components;

namespace GoodsBlazor.Common.Components.LoadingSpinner;

public partial class LoadingSpinnerComponent
{
    private bool _isVisible;

    [Parameter]
    public bool IsVisible
    {
        get => _isVisible;
        set
        {
            if (_isVisible != value)
            {
                _isVisible = value;
                StateHasChanged();
            }
        }
    }
}
