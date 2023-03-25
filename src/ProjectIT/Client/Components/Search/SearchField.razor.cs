using Microsoft.AspNetCore.Components;

namespace ProjectIT.Client.Components.Search;

public partial class SearchField
{
    [Parameter]
    public string? PlaceholderText { get; init; }

    [Parameter]
    public int Height { get; init; } = 30;

    [Parameter]
    public int Width { get; init; }

    [Parameter]
    public EventCallback<string> OnInputValueChanged { get; set; }

    public string? SearchString { get; set; }

    private int SearchIconSize
    {
        get => (int)(Height * 0.6);
    }

    private void OnInput(string newValue)
    {
        SearchString = newValue;
        if (newValue != null && OnInputValueChanged.HasDelegate)
        {
            OnInputValueChanged.InvokeAsync(newValue);
        }
    }
}