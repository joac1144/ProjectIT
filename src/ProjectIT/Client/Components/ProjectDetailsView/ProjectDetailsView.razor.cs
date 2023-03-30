using Microsoft.AspNetCore.Components;

namespace ProjectIT.Client.Components.ProjectDetailsView;

public partial class ProjectDetailsView
{
    [Parameter]
    public string? Title { get; set;}
    
    [Parameter]
    public string? Description { get; set;}
}
