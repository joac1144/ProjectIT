using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using ProjectIT.Shared.Dtos.Requests;
using ProjectIT.Shared.Enums;
using ProjectIT.Shared.Extensions;
using ProjectIT.Shared.Models;
using ProjectIT.Shared.Resources;
using Radzen.Blazor;
using System.Net.Http.Json;
using System.Security.Claims;

namespace ProjectIT.Client.Pages.CreateRequest;

public partial class CreateRequestPage
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

    private IEnumerable<EctsWrapper>? ectsWrappers;

    private IEnumerable<ProgrammeWrapper>? programmeWrappers;

    private IEnumerable<LanguageWrapper>? languageWrappers;

    private RadzenDropDown<Topic>? topicSelector;
    private RadzenDropDown<Supervisor>? supervisorSelector;
    private IEnumerable<Programme>? requestProgrammes;
    private IEnumerable<Language>? requestLanguages;
    private readonly List<Topic> requestTopics = new();
    private readonly List<Supervisor> requestSupervisors = new();
    private int groupMembers = 1;
    private string? email;
    private IEnumerable<Topic> topics = null!;
    private IEnumerable<Supervisor> supervisors = null!;
    private string? topicName;
    private string? supervisorName;
    private readonly Request request = new();
    private ClaimsPrincipal? authUser;

    protected override async Task OnInitializedAsync()
    {
        ectsWrappers = Enum.GetValues<Ects>().Select(ects => new EctsWrapper { Ects = ects, StringValue = ects.GetTranslatedString(EnumsLocalizer) });
        programmeWrappers = Enum.GetValues<Programme>().Select(prog => new ProgrammeWrapper { Programme = prog, StringValue = prog.GetTranslatedString(EnumsLocalizer) });
        languageWrappers = Enum.GetValues<Language>().Select(lang => new LanguageWrapper { Language = lang, StringValue = lang.GetTranslatedString(EnumsLocalizer) });

        authUser = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User;
        
        await getSupervisorsAndTopicsData();
    }

    private async Task getSupervisorsAndTopicsData()
    {
        topics = (await httpClient.Client.GetFromJsonAsync<IEnumerable<Topic>>("topics"))!;
        if (topics == null)
            throw new Exception("Could not load topics");
        supervisors = (await httpClient.Client.GetFromJsonAsync<IEnumerable<Supervisor>>("supervisors"))!;
        if (supervisors == null)
            throw new Exception("Could not load supervisors");
    }

    private void OnTopicSelectedInList(object value)
    {
        if (value != null)
        {
            string val = (string)value;
            requestTopics.Add(topics.Single(t => t.Name == val));
            topics = topics.Where(t => t.Name != val);
            topicSelector?.Reset();
        }
    }

    private void OnSupervisorSelectedInList(object value)
    {
        if (value != null)
        {
            int val = (int)value;
            requestSupervisors.Add(supervisors.Single(s => s.Id == val));
            supervisors = supervisors.Where(s => s.Id != val);
            supervisorSelector?.Reset();
        }
    }

    private void OnSelectedTopicClicked(Topic topic)
    {
        requestTopics.Remove(topic);
        topics = topics.Append(topic);
    }

    private void OnSelectedSupervisorClicked(Supervisor supervisor)
    {
        requestSupervisors.Remove(supervisor);
        supervisors = supervisors.Append(supervisor);
    }

    private void OnAddNewTopicFromSearchClicked()
    {
        if (!string.IsNullOrWhiteSpace(topicName) || topics.Select(topic => topic.Name).Contains(topicName, StringComparer.OrdinalIgnoreCase))
            requestTopics.Add(new Topic { Name = topicName! });
        topicName = string.Empty;
    }
    
    private async void SubmitRequestAsync()
    {
        var requestDto = new RequestCreateDto
        {
            Title = request.Title,
            Description = request.Description,
            Topics = topics,
            Languages = requestLanguages!,
            Programmes = requestProgrammes!,
            Members = new Student[] { },
            Supervisors = supervisors,
            Ects = request.Ects,
            Semester = request.Semester
        };

        var response = await httpClient.Client.PostAsJsonAsync("https://localhost:7094/requests", requestDto);

        if (response.IsSuccessStatusCode)
        {
            navManager.NavigateTo("/");
        }
        else
        {
            //Do something
        }
    }

    private void CancelRequest()
    {
        navManager.NavigateTo("/");
    }
}
