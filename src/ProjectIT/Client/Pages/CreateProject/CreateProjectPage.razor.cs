using System.Net.Http.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using ProjectIT.Client.Components.Filter;
using ProjectIT.Shared.Dtos.Projects;
using ProjectIT.Shared.Models;

namespace ProjectIT.Client.Pages.CreateProject;

public partial class CreateProjectPage
{
    private class TopicGroupData : Topic
    {
        public bool IsGroup => Name != null;
    }

    public IList<Project> Projects { get; set; } = new List<Project>();

    public Project Project { get; set; } = new Project();

    private ClaimsPrincipal authUser;

    private string? descriptionHtml;

    private IEnumerable<TopicGroupData>? topics;
    private IEnumerable<string>? topicNames;
    private Topic? currentTopic;

    protected override async Task OnInitializedAsync()
    {
        authUser = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User;

        topics = (await httpClient.GetFromJsonAsync<IEnumerable<Topic>>("topics"))!.Select(t => new TopicGroupData { Id = t.Id, Name = t.Name, Category = t.Category });

        topics = topics?.GroupBy(t => t.Category)
            .SelectMany(i => new TopicGroupData[] { new TopicGroupData { Category = i.Key } }
                .Concat(i.Select(o
                => new TopicGroupData
                {
                    Name = o.Name
                })));

        topicNames = topics?.Select(t => t.Name);
    }

    private void OnTagClickedInFilterPanel(FilterTag filterTag)
    {
        
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