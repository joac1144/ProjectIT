using System.Net.Http.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using ProjectIT.Client.Components.Filter;
using ProjectIT.Shared.Dtos.Projects;
using ProjectIT.Shared.Enums;
using ProjectIT.Shared.Models;
using Radzen.Blazor;

namespace ProjectIT.Client.Pages.CreateProject;

public partial class CreateProjectPage
{
    private class EctsWrapper
    {
        public Ects Ects { get; set; }

        public string StringValue => Ects.ToString();
    }

    private IEnumerable<Topic> topics;
    private readonly IEnumerable<EctsWrapper> ectsWrappers = Enum.GetValues<Ects>().Select(ects => new EctsWrapper { Ects = ects });

    private readonly Project project = new();
    private string? descriptionHtml;
    private readonly List<Programme> projectProgrammes = new();
    private readonly List<Language> projectLanguages = new();
    private readonly List<Topic> projectTopics = new();

    private RadzenDropDown<Topic>? topicSelector;

    private ClaimsPrincipal? authUser;

    protected override async Task OnInitializedAsync()
    {
        authUser = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User;

        topics = (await httpClient.Client.GetFromJsonAsync<IEnumerable<Topic>>("topics"))!;
        if (topics == null)
            throw new Exception("Could not load topics");

        SortTopics();
    }

    private void OnTagClickedInFilterPanel(FilterTagSimple filterTag)
    {
        if (filterTag.Selected)
        {
            switch (filterTag.FilterType)
            {
                case FilterType.Programme:
                    projectProgrammes.Add((Programme)Enum.Parse(typeof(Programme), filterTag.Tag));
                    break;
                case FilterType.Language:
                    projectLanguages.Add((Language)Enum.Parse(typeof(Language), filterTag.Tag));
                    break;
            }
        }
        else
        {
            switch (filterTag.FilterType)
            {
                case FilterType.Programme:
                    projectProgrammes.Remove((Programme)Enum.Parse(typeof(Programme), filterTag.Tag));
                    break;
                case FilterType.Language:
                    projectLanguages.Remove((Language)Enum.Parse(typeof(Language), filterTag.Tag));
                    break;
            }
        }
    }

    private void OnTopicSelectedInList(object value)
    {
        if (value != null)
        {
            string val = (string)value;
            projectTopics.Add(topics.Single(t => t.Name == val));
            topics = topics.Where(t => t.Name != val);
            topicSelector?.Reset();
        }
    }

    private void OnSelectedTopicClicked(Topic topic)
    {
        projectTopics.Remove(topic);
        topics = topics.Append(topic);
        SortTopics();
    }

    private void SortTopics() => topics = topics.OrderBy(t => nameof(t.Category)).ThenBy(t => t.Name);

    private async Task SubmitProjectAsync()
    {
        var newProject = new ProjectCreateDto()
        {
            Title = project.Title,
            Description = descriptionHtml!,
            Topics = projectTopics.Select(t => new Topic { Name = t.Name, Category = t.Category }),
            Languages = projectLanguages,
            Programmes = projectProgrammes,
            Ects = project.Ects,
            Semester = project.Semester,
            Supervisor = new Supervisor 
            {
                Id = (new Random()).Next(20, 5000),
                FullName = authUser?.Identity?.Name!,
                Email = "jkof@itu.dk",
                Profession = "Professor",
                Topics = new[] { new Topic { Id = (new Random()).Next(30, 5000), Name = "test", Category = TopicCategory.SoftwareEngineering } }
            },
            CoSupervisor = project.CoSupervisor
        };

        var response = await httpClient.Client.PostAsJsonAsync("projects", newProject);

        if (response.IsSuccessStatusCode)
        {
            await JSRuntime.InvokeAsync<string>("alert", "Project created successfully!");
            // Navigate to "my projects" page.
            navManager.NavigateTo("/");
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