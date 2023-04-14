using Microsoft.AspNetCore.Components;

namespace ProjectIT.Client.Components.Search;

public partial class SearchField
{
    [Parameter]
    public string? PlaceholderText { get; init; }

    [Parameter]
    public string Height { get; init; } = "30px";

    [Parameter]
    public string Width { get; init; } = "250px";

    [Parameter]
    public bool ShowSearchIcon { get; init; } = true;

    [Parameter]
    public EventCallback<string> OnInputValueChanged { get; set; }

    public string? SearchString { get; set; }

    private void OnInput(string newValue)
    {
        SearchString = newValue;
        if (newValue != null && OnInputValueChanged.HasDelegate)
        {
            OnInputValueChanged.InvokeAsync(newValue);
        }
    }
}