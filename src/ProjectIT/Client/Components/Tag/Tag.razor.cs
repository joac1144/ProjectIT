using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace ProjectIT.Client.Components.Tag;

public partial class Tag
{
    [Parameter]
    public string Text { get; set; } = string.Empty;

    [Parameter]
    public bool IsDisabled { get; set; }

    [Parameter]
    public EventCallback<MouseEventArgs> OnClicked { get; set; }
}