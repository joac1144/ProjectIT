using Microsoft.AspNetCore.Components;
using ProjectIT.Client.Components.Filter;
using ProjectIT.Shared.Dtos.Projects;
using System.Net.Http.Json;

namespace ProjectIT.Client.Pages;

public partial class ProjectDetails
{

    [Parameter]
    public int id { get; set; }

    public ProjectDetailsDto project { get; set; }
    HttpClient httpClient = new HttpClient();
    public IList<FilterTag> Tags { get; set; } = new List<FilterTag>();

    private void ApplyProject(NavigationManager navigationManager)
    {
        navigationManager.NavigateTo("/");
    }

    // protected override async Task OnInitializedAsync()
    // {
    //     project = await httpClient.GetFromJsonAsync<ProjectDetailsDto>("https://localhost:7094/projects/1");

    // }

}