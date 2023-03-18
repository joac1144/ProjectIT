using Microsoft.AspNetCore.Components;

namespace ProjectIT.Client.Shared;

public partial class Tag
{
    [Parameter]
    public string Text { get; set; } = string.Empty;

    private void OnTagClicked()
    {
        // Logic to remove tag from current search.
    }
}