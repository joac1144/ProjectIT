using System.Net.Http.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
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

    private class ProgrammeWrapper
    {
        public Programme Programme { get; set; }
        public string StringValue => Programme.ToString();
    }

    private class LanguageWrapper
    {
        public Language Language { get; set; }
        public string StringValue => Language.ToString();
    }

    private IEnumerable<Topic> topics;
    private readonly IEnumerable<EctsWrapper> ectsWrappers = Enum.GetValues<Ects>().Select(ects => new EctsWrapper { Ects = ects });
    private readonly IEnumerable<ProgrammeWrapper> programmeWrappers = Enum.GetValues<Programme>().Select(prog => new ProgrammeWrapper { Programme = prog });
    private readonly IEnumerable<LanguageWrapper> languageWrappers = Enum.GetValues<Language>().Select(lang => new LanguageWrapper { Language = lang });

    private readonly Project project = new();
    private string? descriptionHtml;
    private IEnumerable<Programme>? projectProgrammes;
    private IEnumerable<Language>? projectLanguages;
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

    private void SortTopics() => topics = topics.OrderBy(t => t.Category.ToString()).ThenBy(t => t.Name);

    private void OnAddTopicButtonClicked()
    {
        // Add ability to add new topic.
    }

    private async Task SubmitProjectAsync()
    {
        var newProject = new ProjectCreateDto()
        {
            Title = project.Title,
            Description = descriptionHtml!,
            Topics = projectTopics.Select(t => new Topic { Name = t.Name, Category = t.Category }),
            Languages = projectLanguages!,
            Programmes = projectProgrammes!,
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

        if (newProject.Topics.Select(t => t.Name).Except(topics.Select(t => t.Name)).Any())
        {
            // A new topic was added, open dialog to confirm and to add category.
        }

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