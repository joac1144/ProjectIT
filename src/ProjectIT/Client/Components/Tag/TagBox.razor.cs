using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ProjectIT.Client.Components.Filter;

namespace ProjectIT.Client.Components.Tag;

public partial class TagBox
{
    [Parameter]
    public FilterTag Tag { get; set; } = null!;

    [Parameter]
    public bool IsDisabled { get; set; }

    [Parameter]
    public EventCallback<MouseEventArgs> OnClicked { get; set; }
}