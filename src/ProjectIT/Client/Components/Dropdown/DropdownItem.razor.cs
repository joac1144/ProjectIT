using Microsoft.AspNetCore.Components;

namespace ProjectIT.Client.Components.Dropdown;

public partial class DropdownItem
{
    [Parameter]
    public string Text { get; set; } = string.Empty;

    [Parameter]
    public bool OpensModal { get; set; }

    [Parameter]
    public string? ModalIdentifier { get; set; }

    [Parameter]
    public EventCallback OnClick { get; set; }
}