using Microsoft.AspNetCore.Components;
using ProjectIT.Client.Components.Filter;
using ProjectIT.Shared.Dtos.Projects;
using ProjectIT.Shared.Models;
using System.Net.Http.Json;

namespace ProjectIT.Client.Pages;

public partial class ProjectDetails
{
    //TODO: for later use, don't delete
    [Parameter]
    public int Id { get; set; }

    private ProjectDetailsDto? project;

    public IList<FilterTag>? Topics { get; set; }

    private void ApplyProject(NavigationManager navigationManager)
    {
        navigationManager.NavigateTo("/");
    }

    protected override async Task OnInitializedAsync()
    {
        project = await httpClient.GetFromJsonAsync<ProjectDetailsDto>("projects/999");

        if (project != null)
        {
            List<FilterTag> tags = new List<FilterTag>();

            foreach (Topic topic in project?.Topics!)
            {
                FilterTag tag = new()
                {
                    Tag = topic.Name,
                    Selected = true
                };
                tags.Add(tag);
            }

            Topics = tags;
        }
    }
}