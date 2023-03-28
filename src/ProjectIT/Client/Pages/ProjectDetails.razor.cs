using Microsoft.AspNetCore.Components;
using ProjectIT.Client.Components.Filter;

namespace ProjectIT.Client.Pages;

public partial class ProjectDetails 
{

    public IList<FilterTag> Tags { get; set; } = new List<FilterTag>();

    private void ApplyProject(NavigationManager navigationManager)
    {
        navigationManager.NavigateTo("/");
    }
    
}