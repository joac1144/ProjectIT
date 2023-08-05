using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Localization;
using ProjectIT.Client.Constants;
using ProjectIT.Shared;
using ProjectIT.Shared.Dtos.Requests;
using ProjectIT.Shared.Enums;
using ProjectIT.Shared.Extensions;
using ProjectIT.Shared.Models;
using ProjectIT.Shared.Resources;
using Radzen.Blazor;
using System.Net.Http.Json;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace ProjectIT.Client.Pages.Students.CreateRequest;

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
    private readonly List<Student> ExtraMembers = new();
    private readonly int groupMembers = 1;
    private string? email;
    private IEnumerable<Topic> topics = null!;
    private IEnumerable<Supervisor> supervisors = null!;
    private IEnumerable<Student> students = null!;
    private string? topicName;
    private string? memberMail;
    private string? descriptionHtml;
    private Ects? requestEcts;
    private readonly Request request = new();

    private ClaimsPrincipal? authUser;
    private string? userEmail;

    protected override async Task OnInitializedAsync()
    {
        ectsWrappers = Enum.GetValues<Ects>().Select(ects => new EctsWrapper { Ects = ects, StringValue = ects.GetTranslatedString(EnumsLocalizer) });
        programmeWrappers = Enum.GetValues<Programme>().Select(prog => new ProgrammeWrapper { Programme = prog, StringValue = prog.GetTranslatedString(EnumsLocalizer) });
        languageWrappers = Enum.GetValues<Language>().Select(lang => new LanguageWrapper { Language = lang, StringValue = lang.GetTranslatedString(EnumsLocalizer) });

        authUser = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User;
        userEmail = authUser?.FindFirst("preferred_username")?.Value!;

        await GetData();
        SortTopics();
        SortSupervisors();
    }

    private async Task GetData()
    {
        topics = (await httpClient.Client.GetFromJsonAsync<IEnumerable<Topic>>(ApiEndpoints.Topics))!;
        if (topics == null)
            throw new Exception("Could not load topics");
        supervisors = (await httpClient.Client.GetFromJsonAsync<IEnumerable<Supervisor>>(ApiEndpoints.Supervisors))!;
        if (supervisors == null)
            throw new Exception("Could not load supervisors");
        students = (await httpClient.Client.GetFromJsonAsync<IEnumerable<Student>>(ApiEndpoints.Students))!;
        if (students == null)
            throw new Exception("Could not load students");
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
        if (value is not null)
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
        SortTopics();
    }

    private void OnSelectedSupervisorClicked(Supervisor supervisor)
    {
        requestSupervisors.Remove(supervisor);
        supervisors = supervisors.Append(supervisor);
        SortSupervisors();
    }

    private void SortTopics() => topics = topics.OrderBy(t => t.Category.ToString()).ThenBy(t => t.Name);

    private void SortSupervisors() => supervisors = supervisors.OrderBy(s => s.FullName);

    private void OnAddNewTopicFromSearchClicked()
    {
        if (!string.IsNullOrWhiteSpace(topicName)) {
            if (topics.Select(topic => topic.Name).Contains(topicName, StringComparer.OrdinalIgnoreCase))
            {
                var newTopic = topics.Single(topic => topic.Name.Equals(topicName, StringComparison.OrdinalIgnoreCase));
                requestTopics.Add(newTopic);
                topics = topics.Where(topic => topic.Name != newTopic.Name);
                topicSelector?.Reset();
            }
            else
                requestTopics.Add(new Topic { Name = topicName! });
        }
        topicName = string.Empty;
    }

    private async Task OnAddNewMemberFromSearchClicked()
    {
        if (!string.IsNullOrWhiteSpace(memberMail) && Regex.IsMatch(memberMail, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$") ) {
            if (!ExtraMembers!.Select(member => member.Email).Contains(memberMail, StringComparer.OrdinalIgnoreCase) && students.Select(student => student.Email).Contains(memberMail, StringComparer.OrdinalIgnoreCase))
            {
                var newMember = students.Single(student => student.Email.Equals(memberMail, StringComparison.OrdinalIgnoreCase));
                ExtraMembers.Add(newMember);
                students = students.Where(student => student.Email != newMember.Email);
            }
            else
            {
                await JSRuntime.InvokeAsync<string>("alert", "The student is already added to the project request or does not exist.");
            }
        }
        else
        {
            await JSRuntime.InvokeAsync<string>("alert", "Please enter a valid email address.");
        }
        memberMail = string.Empty;
    }

    private void OnSelectedMemberClicked(Student student)
    {
        ExtraMembers.Remove(student);
        students = students.Append(student);
    }

    private async void SubmitRequestAsync()
    {
        try 
        {
            var studentNameSplit = authUser?.Identity?.Name?.Split(" ");

            var newRequest = new RequestCreateDto
            {
                Title = request.Title,
                DescriptionHtml = descriptionHtml!,
                Topics = requestTopics.Select(t => new Topic { Name = t.Name, Category = t.Category }),
                Languages = requestLanguages!,
                Programmes = requestProgrammes!,
                StudentEmail = userEmail!,
                ExtraMembersEmails = ExtraMembers?.Select(m => m.Email),
                SupervisorEmails = requestSupervisors.Select(s => s.Email),
                Ects = (Ects)requestEcts!,
                Semester = request.Semester,
                Status = RequestStatus.Pending
            };

            var response = await httpClient.Client.PostAsJsonAsync(ApiEndpoints.Requests, newRequest);

            if (response.IsSuccessStatusCode)
            {
                await JSRuntime.InvokeAsync<string>("alert", "Request created successfully!");
                navManager.NavigateTo(PageUrls.MyRequests);
            }
            else
            {
                await JSRuntime.InvokeAsync<string>("alert", "Something went wrong! Please make sure to fill out all required fields and try again.");
            }
        }
        catch
        {
            await JSRuntime.InvokeAsync<string>("alert", "Something went wrong! Please make sure to fill out all required fields and try again.");
        }
    }

    private void CancelRequest() => navManager.NavigateTo(PageUrls.MyRequests);
}