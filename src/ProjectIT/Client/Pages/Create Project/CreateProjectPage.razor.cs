using System.Net.Http.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using ProjectIT.Client.Components.Filter;
using ProjectIT.Shared.Dtos.Projects;
using ProjectIT.Shared.Enums;
using ProjectIT.Shared.Models;

namespace ProjectIT.Client.Pages.Create_Project;

public partial class CreateProjectPage
{
    public IList<FilterTag> Tags { get; set; } = new List<FilterTag>();

    public IList<Project> Projects { get; set; } = new List<Project>();

    public Project Project { get; set; } = new Project();

    private ClaimsPrincipal authUser;

    private string? descriptionHtml;

    private string? tagName;

    private IEnumerable<Topic>? topics;
    private IEnumerable<string>? topicNames;

    protected override async Task OnInitializedAsync()
    {
        authUser = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User;

        topics = await httpClient.GetFromJsonAsync<IEnumerable<Topic>>("topics");
        topicNames = topics?.Select(t => t.Name);
    }

    private void FilterPanelsInitialized(IList<FilterTagSimple> data)
    {
        Tags = Tags.Concat(data).ToList();
    }

    private void OnTagClickedInFilterPanel(FilterTag filterTag)
    {
        Tags.Where(ft => ft.Tag == filterTag.Tag).Single().Selected = filterTag.Selected;
    }

    private async Task SubmitProjectAsync()
    {
        var project = new ProjectCreateDto()
        {
            Title = Project.Title,
            Description = descriptionHtml!,
            Topics = Project.Topics,
            Languages = Project.Languages,
            Programmes = Project.Programmes,
            Ects = Project.Ects,
            Semester = Project.Semester,
            Supervisor = Project.Supervisor,
            CoSupervisor = Project.CoSupervisor
        };

        var response = await httpClient.PostAsJsonAsync("projects", project);

        if (response.IsSuccessStatusCode)
        {
            await JSRuntime.InvokeAsync<string>("alert", "Project created successfully!");
            navManager.NavigateTo("MainPage");
        }
        else
        {
            await JSRuntime.InvokeAsync<string>("alert", "Something went wrong, check your input and try again!");
        }
    }

    private void CancelProjectAsync()
    {
        navManager.NavigateTo("/");
    }
}