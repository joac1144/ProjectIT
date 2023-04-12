using Microsoft.AspNetCore.Components;

namespace ProjectIT.Client.Components.Dropdown;

public partial class Dropdown
{
    [Parameter]
    public string? Text { get; set; }

    [Parameter]
    public string? BootstrapIcon { get; set; }

    [Parameter]
    public RenderFragment? DropdownItems { get; set; }
}