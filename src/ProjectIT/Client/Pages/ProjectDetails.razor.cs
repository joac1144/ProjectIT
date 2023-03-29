using Microsoft.AspNetCore.Components;
using ProjectIT.Client.Components.Filter;
using ProjectIT.Shared.Dtos.Projects;
using System.Net.Http.Json;

namespace ProjectIT.Client.Pages;

public partial class ProjectDetails
{
    //TODO: for later use, don't delete
    [Parameter]
    public int Id { get; set; }

    private ProjectDetailsDto? project;

    private HttpClient HttpClient = new HttpClient();

    public IList<FilterTag> Tags { get; set; } = new List<FilterTag>();

    private void ApplyProject(NavigationManager navigationManager)
    {
        navigationManager.NavigateTo("/");
    }

    protected override async Task OnInitializedAsync()
    {
        project = await HttpClient.GetFromJsonAsync<ProjectDetailsDto>("https://localhost:7094/projects/1");
    }
}