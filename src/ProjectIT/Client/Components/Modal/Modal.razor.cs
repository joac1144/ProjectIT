using Microsoft.AspNetCore.Components;

namespace ProjectIT.Client.Components.Modal;

public partial class Modal<T>
{
    [Parameter]
    public string Identifier { get; set; } = string.Empty;

    [Parameter]
    public string Title { get; set; } = string.Empty;

    [Parameter]
    public string? CloseButtonText { get; set; }

    [Parameter]
    public RenderFragment? Body { get; set; }
    
    [Parameter]
    public RenderFragment? Buttons { get; set; }

    public T Data { get; set; } = default!;

    private string displayClass = "none";

    public void OpenModal(T data)
    {
        Data = data;
        displayClass = "block;";
        StateHasChanged();
    }

    public void Hide()
    {
        Data = default!;
        displayClass = "none;";
        StateHasChanged();
    }
}