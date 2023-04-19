using System.Net.Http.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using ProjectIT.Shared;
using ProjectIT.Shared.Dtos.Projects;
using ProjectIT.Shared.Enums;
using ProjectIT.Shared.Extensions;
using ProjectIT.Shared.Models;
using ProjectIT.Shared.Resources;
using Radzen.Blazor;

namespace ProjectIT.Client.Pages.Supervisors.UpdateProject;

public partial class UpdateProjectPage
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

    [Parameter]
    public int Id { get; set; }

    [Inject]
    private IStringLocalizer<EnumsResource> EnumsLocalizer { get; set; } = default!;

    private IEnumerable<Topic> topics = null!;
    private IEnumerable<Supervisor> coSupervisors = null!;
    private IEnumerable<EctsWrapper>? ectsWrappers;
    private IEnumerable<ProgrammeWrapper>? programmeWrappers;
    private IEnumerable<LanguageWrapper>? languageWrappers;

    private ProjectUpdateDto? projectToBeUpdated = new();
    private List<Topic> projectTopics = new();
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

        projectToBeUpdated = await httpClient.Client.GetFromJsonAsync<ProjectUpdateDto>($"{ApiEndpoints.Projects}/{Id}");

        projectTopics = projectToBeUpdated!.Topics.ToList();
        projectCoSupervisor = projectToBeUpdated.CoSupervisor;
        
        await getSupervisorsAndTopicsData();
        UnionTopicsListWithUpdatedProjectTopics();
        SortTopics();
        SortSupervisors();
    }

    private async Task getSupervisorsAndTopicsData()
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

    private void UnionTopicsListWithUpdatedProjectTopics() {
        topics = topics.Union(projectTopics.Select(t => new Topic { Name = t.Name }));

        SortTopics();
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

    private void SortSupervisors() => coSupervisors = coSupervisors.OrderBy(s => s.FirstName).ThenBy(s => s.LastName);

    private void OnAddNewTopicFromSearchClicked() 
    {
        if (!string.IsNullOrWhiteSpace(topicName) || topics.Select(topic => topic.Name).Contains(topicName, StringComparer.OrdinalIgnoreCase))
            projectTopics.Add(new Topic { Name = topicName! });
        topicName = string.Empty;
    }

    private async Task SubmitProjectAsync()
    {
        var updatedProject = new ProjectUpdateDto()
        {
            Id = projectToBeUpdated!.Id,
            Title = projectToBeUpdated!.Title,
            DescriptionHtml = projectToBeUpdated.DescriptionHtml,
            Topics = projectToBeUpdated.Topics.Select(t => new Topic { Name = t.Name, Category = t.Category }),
            Languages = projectToBeUpdated.Languages,
            Programmes = projectToBeUpdated.Programmes,
            Ects = projectToBeUpdated.Ects,
            Semester = projectToBeUpdated.Semester,
            Supervisor = projectToBeUpdated.Supervisor,
            CoSupervisor = projectToBeUpdated.CoSupervisor
        };

        if (updatedProject.Topics.Select(t => t.Name).Except(topics.Select(t => t.Name)).Any())
        {
            // A new topic was added, open dialog to confirm and to add category.
        }

        var response = await httpClient.Client.PutAsJsonAsync(ApiEndpoints.Projects, updatedProject);

        if (response.IsSuccessStatusCode)
        {
            await JSRuntime.InvokeAsync<string>("alert", "Project created successfully!");
            navManager.NavigateTo("my-projects");
        }
        else
        {
            await JSRuntime.InvokeAsync<string>("alert", "Something went wrong, check your input and try again!");
        }
    }

    private void CancelProjectAsync()
    {
        navManager.NavigateTo("my-projects");
    }
}