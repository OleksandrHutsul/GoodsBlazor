using Microsoft.AspNetCore.Components;

namespace GoodsBlazor.Common.Components.BCHSelect;

public partial class BCHSelectComponent<T>
{
    [Parameter] public required List<T> Options { get; set; }
    [Parameter] public required EventCallback<T> SelectedChanged { get; set; }

    private bool _isOpened;
    private T? _selectedItem;

    private void ToggleDropdown()
    {
        _isOpened = !_isOpened;
    }

    private async Task SelectItem(T item)
    {
        _selectedItem = item;
        _isOpened = false;
        await SelectedChanged.InvokeAsync(_selectedItem); 
        StateHasChanged();
    }

    private string GetDisplayValue(T? item) => item?.ToString() ?? "Select an option";
}
