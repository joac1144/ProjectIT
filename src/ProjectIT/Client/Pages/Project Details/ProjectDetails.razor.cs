using Microsoft.AspNetCore.Components;
using ProjectIT.Shared.Dtos.Projects;
using System.Net.Http.Json;

namespace ProjectIT.Client.Pages.Project_Details;

public partial class ProjectDetails
{
    [Parameter]
    public int Id { get; set; }

    private ProjectDetailsDto? project;
    
    private string panelWidth = "250px";

    private void ApplyProject(NavigationManager navigationManager)
    {
        navigationManager.NavigateTo("/");
    }

    protected override async Task OnInitializedAsync()
    {
        project = await httpClient.GetFromJsonAsync<ProjectDetailsDto>($"projects/{Id}");
    }
}