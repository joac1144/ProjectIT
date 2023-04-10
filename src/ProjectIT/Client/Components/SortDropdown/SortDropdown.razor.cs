using Microsoft.AspNetCore.Components;
using ProjectIT.Client.Shared.Enums;

namespace ProjectIT.Client.Components.SortDropdown;

public partial class SortDropdown
{
    [Parameter]
    public object? SortValue { get; set; }

    [Parameter]
    public EventCallback<object> SortValueChanged { get; set; }

    [Parameter]
    public IEnumerable<Sort> SortValues { get; set; } = null!;

    private void OnChange(object value)
    {
        SortValue = value;
        SortValueChanged.InvokeAsync(value);
    }
}