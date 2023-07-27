using System.Net.Http.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using ProjectIT.Client.Constants;
using ProjectIT.Shared;
using ProjectIT.Shared.Dtos.Projects;
using ProjectIT.Shared.Enums;
using ProjectIT.Shared.Extensions;
using ProjectIT.Shared.Models;
using ProjectIT.Shared.Resources;
using Radzen.Blazor;

namespace ProjectIT.Client.Pages.Supervisors.CreateProject;

public partial class CreateProjectPage
{
    private class EctsWrapper
    {
        public Ects Ects { get; set; }
        public string StringValue { get; set; } = string.Empty;
    }

    private class ProgrammeWrapper
    {
        public Programme Programme { get; set; }
        public string StringValue { get; set; } = string.Empty;
    }

    private class LanguageWrapper
    {
        public Language Language { get; set; }
        public string StringValue { get; set; } = string.Empty;
    }

    [Inject]
    private IStringLocalizer<EnumsResource> EnumsLocalizer { get; set; } = default!;

    private IEnumerable<Topic> topics = null!;
    private IEnumerable<Supervisor> coSupervisors = null!;
    private IEnumerable<EctsWrapper>? ectsWrappers;
    private IEnumerable<ProgrammeWrapper>? programmeWrappers;
    private IEnumerable<LanguageWrapper>? languageWrappers;

    private readonly Project project = new();
    private string? descriptionHtml;
    private Ects? projectEcts;
    private IEnumerable<Programme>? projectProgrammes;
    private IEnumerable<Language>? projectLanguages;
    private readonly List<Topic> projectTopics = new();
    private Supervisor? projectCoSupervisor = null!;

    private RadzenDropDown<Topic>? topicSelector;
    private RadzenDropDown<Supervisor>? coSupervisorSelector;

    private string? topicName;

    private ClaimsPrincipal? authUser;

    protected override async Task OnInitializedAsync()
    {
        ectsWrappers = Enum.GetValues<Ects>().Select(ects => new EctsWrapper { Ects = ects, StringValue = ects.GetTranslatedString(EnumsLocalizer) });
        programmeWrappers = Enum.GetValues<Programme>().Select(prog => new ProgrammeWrapper { Programme = prog, StringValue = prog.GetTranslatedString(EnumsLocalizer) });
        languageWrappers = Enum.GetValues<Language>().Select(lang => new LanguageWrapper { Language = lang, StringValue = lang.GetTranslatedString(EnumsLocalizer) });

        authUser = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User;

        await GetSupervisorsAndTopicsData();
        SortTopics();
        SortSupervisors();
    }

    private async Task GetSupervisorsAndTopicsData()
    {
        topics = (await httpClient.Client.GetFromJsonAsync<IEnumerable<Topic>>(ApiEndpoints.Topics))!;
        if (topics == null)
            throw new Exception("Could not load topics");
        coSupervisors = (await httpClient.Client.GetFromJsonAsync<IEnumerable<Supervisor>>(ApiEndpoints.Supervisors))!;
        if (coSupervisors == null)
            throw new Exception("Could not load supervisors");
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

    private void OnSupervisorSelectedInList(object value)
    {
        if (value is not null)
        {
            int val = (int)value;
            if (projectCoSupervisor is not null)
            {
                coSupervisors = coSupervisors.Append(projectCoSupervisor);
                SortSupervisors();
            }
            projectCoSupervisor = coSupervisors.Single(s => s.Id == val);
            coSupervisors = coSupervisors.Where(s => s.Id != val);
            coSupervisorSelector?.Reset();
        }
    }

    private void OnSelectedTopicClicked(Topic topic)
    {
        projectTopics.Remove(topic);
        topics = topics.Append(topic);
        SortTopics();
    }

    private void OnSelectedCoSupervisorClicked(Supervisor coSupervisor)
    {
        projectCoSupervisor = null;
        coSupervisors = coSupervisors.Append(coSupervisor);
        SortSupervisors();
    }

    private void SortTopics() => topics = topics.OrderBy(t => t.Category.ToString()).ThenBy(t => t.Name);

    private void SortSupervisors() => coSupervisors = coSupervisors.OrderBy(s => s.FullName);

    private void OnAddNewTopicFromSearchClicked() 
    {
        if (!string.IsNullOrWhiteSpace(topicName)) {
            if (topics.Select(topic => topic.Name).Contains(topicName, StringComparer.OrdinalIgnoreCase))
            {
                var newTopic = topics.Single(topic => topic.Name.Equals(topicName, StringComparison.OrdinalIgnoreCase));
                projectTopics.Add(newTopic);
                topics = topics.Where(topic => topic.Name != newTopic.Name);
                topicSelector?.Reset();
            }
            else
                projectTopics.Add(new Topic { Name = topicName! });
        }
        topicName = string.Empty;
    }

    private void AssignCoSupervisorIfNotNull()
    {
        // If projectcosupervisor is null, newProject.CoSupervisor should be null. Otherwise, create a new instance of the newProject.CoSupervisor with the values given from projectcosupervisor.
        project.CoSupervisor = projectCoSupervisor is not null ? new Supervisor { Id = projectCoSupervisor.Id, FirstName = projectCoSupervisor.FirstName, LastName = projectCoSupervisor.LastName } : null;
    }

    private async Task SubmitProjectAsync()
    {
        var superviserNameSplit = authUser?.Identity?.Name?.Split(" ");

        var newProject = new ProjectCreateDto()
        {
            Title = project.Title,
            DescriptionHtml = descriptionHtml!,
            Topics = projectTopics.Select(t => new Topic { Name = t.Name, Category = t.Category }),
            Languages = projectLanguages!,
            Programmes = projectProgrammes!,
            Ects = (Ects)projectEcts!,
            Semester = project.Semester,
            Supervisor = new()
            {
                Id = (new Random()).Next(30, 10000),
                FirstName = string.Join(" ", superviserNameSplit?.Take(superviserNameSplit.Length - 1)!),
                LastName = superviserNameSplit?.Last()!,
                Email = authUser?.FindFirst("preferred_username")?.Value!,
                Profession = SupervisorProfession.FullProfessor,
                Status = SupervisorStatus.Available,
                Topics = new[] { new Topic { Id = (new Random()).Next(30, 5000), Name = "topicMadeByProjectCreation", Category = TopicCategory.SoftwareEngineering } }
            },
            CoSupervisor = projectCoSupervisor
        };

        if (newProject.Topics.Select(t => t.Name).Except(topics.Select(t => t.Name)).Any())
        {
            // A new topic was added, open dialog to confirm and to add category.
        }

        var response = await httpClient.Client.PostAsJsonAsync(ApiEndpoints.Projects, newProject);

        if (response.IsSuccessStatusCode)
        {
            await JSRuntime.InvokeAsync<string>("alert", "Project created successfully!");
            navManager.NavigateTo(PageUrls.MyProjects);
        }
        else
        {
            await JSRuntime.InvokeAsync<string>("alert", "Something went wrong, check your input and try again!");
        }
    }

    private void CancelProjectAsync() => navManager.NavigateTo(PageUrls.MyProjects);
}