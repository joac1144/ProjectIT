using ProjectIT.Shared.Enums;
using Radzen.Blazor;
using ProjectIT.Shared.Extensions;
using ProjectIT.Shared.Dtos.Topics;
using System.Net.Http.Json;
using ProjectIT.Shared;
using ProjectIT.Shared.Models;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Security.Claims;
using ProjectIT.Shared.Dtos.Users;
using ProjectIT.Client.Constants;

namespace ProjectIT.Client.Pages.Supervisors.ProfilePage;

public partial class SupervisorProfile
{
    private IEnumerable<string> professions = new List<string>();
    private IEnumerable<string> statuses = new List<string>();
    private SupervisorStatus supervisorStatus;
    private RadzenDropDown<Topic>? topicSelector;
    private IEnumerable<Topic> topics = null!;
    private IEnumerable<Topic> topicsInDropdownList = null!;
    private IEnumerable<Topic> topicsInDropdownListInitial = null!;
    private string topicName = string.Empty;
    private SupervisorDetailsDto supervisor = new();
    //private List<Topic> supervisorTopics = new();

    private List<Topic> existingSupervisorTopics = new();
    private readonly List<Topic> newTopics = new();

    private ClaimsPrincipal? authUser;
    private bool isLoading = false;

    protected override async Task OnInitializedAsync()
    {
        authUser = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User;
        string userEmail = authUser?.FindFirst("preferred_username")?.Value!;

        isLoading = true;
        supervisor = (await httpClient.GetFromJsonAsync<IEnumerable<SupervisorDetailsDto>>(ApiEndpoints.Supervisors))!.Where(supervisor => supervisor.Email.Equals(userEmail, StringComparison.OrdinalIgnoreCase)).FirstOrDefault()!;
        isLoading = false;

        //supervisorTopics = supervisor.Topics?.ToList() ?? new List<Topic>();
        existingSupervisorTopics = supervisor.Topics?.ToList() ?? new List<Topic>();

        await GetStatusAndProfessionAndTopicsData();
        SortTopics();
    }

    private async Task GetStatusAndProfessionAndTopicsData()
    {
        topics = (await httpClient.GetFromJsonAsync<IEnumerable<Topic>>(ApiEndpoints.Topics))!;
        if (topics == null)
            throw new Exception("Could not load topics");

        topicsInDropdownList = topics.Where(topic => !existingSupervisorTopics.Select(t => t.Name).Contains(topic.Name));
        topicsInDropdownListInitial = topicsInDropdownList;

        professions = Enum.GetValues<SupervisorProfession>().ToList().Select(sp => sp.GetTranslatedString(EnumsLocalizer)).ToList();
        statuses = Enum.GetValues<SupervisorStatus>().ToList().Select(ss => ss.GetTranslatedString(EnumsLocalizer)).ToList();
    }

    private void OnProfessionsSelectedInList(object value)
    {
        if (value is not null)
        {
            string val = (string)value;
            var professions = Enum.GetValues<SupervisorProfession>();
            foreach (var profession in professions)
            {
                if (profession.GetTranslatedString(EnumsLocalizer) == val)
                {
                    supervisor.Profession = profession;
                    break;
                }
            }
        }
    }

    private void OnStatusesSelectedInList(object value)
    {
        string val = (string)value;
        var statuses = Enum.GetValues<SupervisorStatus>();
        foreach (var status in statuses)
        {
            if (status.GetTranslatedString(EnumsLocalizer) == val)
            {
                supervisor.Status = status;
                break;
            }
        }
    }

    private void OnTopicSelectedInList(object value)
    {
        if (value is not null)
        {
            string val = (string)value;
            existingSupervisorTopics.Add(topicsInDropdownList.Single(t => t.Name == val));
            topicsInDropdownList = topicsInDropdownList.Where(t => t.Name != val);
            topicSelector?.Reset();
        }
    }

    private void OnAddNewTopicFromSearchClicked()
    {
        if (!string.IsNullOrWhiteSpace(topicName)) {
            if (topicName.Length > 25)
            {
                JSRuntime.InvokeAsync<string>("alert", "Topic should not be more than 25 characters");
                topicName = string.Empty;
            }
            if (topicsInDropdownList.Select(topic => topic.Name).Contains(topicName, StringComparer.OrdinalIgnoreCase))
            {
                var newTopic = topicsInDropdownList.Single(topic => topic.Name.Equals(topicName, StringComparison.OrdinalIgnoreCase));
                existingSupervisorTopics.Add(newTopic);
                topicsInDropdownList = topicsInDropdownList.Where(topic => topic.Name != newTopic.Name);
                topicSelector?.Reset();
            }
            else
                newTopics.Add(new Topic { Name = topicName });
        }
        topicName = string.Empty;
    }

    private void OnSelectedTopicClicked(Topic topic)
    {
        if (newTopics.Contains(topic))
        {
            newTopics.Remove(topic);
        }
        else
        {
            existingSupervisorTopics.Remove(topic);
            topicsInDropdownList = topicsInDropdownList.Append(topic);
            SortTopics();
        }
    }

    private void SortTopics() => 
        topicsInDropdownList = topicsInDropdownList.OrderBy(t => t.Category.ToString()).ThenBy(t => t.Name);

    private async Task SubmitAsync()
    {
        try
        {
            var supervisorDetails = new SupervisorDetailsDto()
            {
                Id = supervisor.Id,
                FirstName = supervisor.FirstName,
                LastName = supervisor.LastName,
                Email = supervisor.Email,
                Status = supervisor.Status,
                Profession = supervisor.Profession,
                Topics = existingSupervisorTopics.Union(newTopics)
            };

            if (newTopics.Any())
            {
                // A new topic was added, open dialog to confirm and to add category.
                var result = await SelectTopicCategoryDialog(newTopics);

                // Check if the dialog was confirmed (Save button clicked)
                // and update the project's topics with the modified newProject topics.
                if (result == true)
                {
                    existingSupervisorTopics = existingSupervisorTopics.Concat(newTopics).ToList();
                    newTopics.Clear();
                }
                else
                {
                    // If the dialog was canceled (Cancel button clicked), do not post the project.
                    return;
                }
            }

            var response = await anonymousClient.Client.PutAsJsonAsync($"{ApiEndpoints.Supervisors}", supervisorDetails);

            if (response.IsSuccessStatusCode)
            {
                await JSRuntime.InvokeAsync<string>("alert", "Supervisor profile updated successfully!");
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

    private void DiscardChanges()
    {
        // Reset topics
        existingSupervisorTopics = supervisor.Topics?.ToList() ?? new List<Topic>();
        newTopics.Clear();
        topicsInDropdownList = topicsInDropdownListInitial;
        SortTopics();

        // Reset status dropdown
        supervisor.Status = supervisorStatus;
    }
}