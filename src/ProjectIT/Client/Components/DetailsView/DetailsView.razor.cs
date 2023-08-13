using Microsoft.AspNetCore.Components;

namespace ProjectIT.Client.Components.DetailsView;

public partial class DetailsView
{
    [Parameter]
    public string? Title { get; set;}
    
    [Parameter]
    public string? Description { get; set;}
}
