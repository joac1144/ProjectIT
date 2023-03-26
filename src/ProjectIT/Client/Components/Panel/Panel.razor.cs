using Microsoft.AspNetCore.Components;

namespace ProjectIT.Client.Components.Panel;

public partial class Panel
{
    [Parameter]
    public string Title { get; set; } = "Placeholder title";

    [Parameter]
    public string Width { get; set; } = "320px";

    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}