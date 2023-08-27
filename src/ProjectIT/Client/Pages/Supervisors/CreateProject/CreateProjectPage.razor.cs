using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Localization;
using Microsoft.Graph.Models;
using Microsoft.JSInterop;
using Newtonsoft.Json;
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

    private readonly Dictionary<string, string> _htmlEntitiesTable = new()
    {
        { "&nbsp;", " " },
        { "&amp;", "&" },
        { "&quot;", "\"" },
        { "&apos;", "'" },
        { "&lt;", "<" },
        { "&gt;", ">" },
        { "&cent;", "¢" },
        { "&pound;", "£" },
        { "&yen;", "¥" },
        { "&euro;", "€" },
        { "&copy;", "©" },
        { "&reg;", "®" },
        { "&trade;", "™" },
        { "&times;", "×" },
        { "&divide;", "÷" }
    };

    // All topics in database.
    private IEnumerable<Topic> topics = null!;

    // All topics available in the dropdown.
    private IEnumerable<Topic> topicsInDropdownList = null!;

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
    private string? userEmail;

    protected override async Task OnInitializedAsync()
    {
        ectsWrappers = Enum.GetValues<Ects>().Select(ects => new EctsWrapper { Ects = ects, StringValue = ects.GetTranslatedString(EnumsLocalizer) });
        programmeWrappers = Enum.GetValues<Programme>().Select(prog => new ProgrammeWrapper { Programme = prog, StringValue = prog.GetTranslatedString(EnumsLocalizer) });
        languageWrappers = Enum.GetValues<Language>().Select(lang => new LanguageWrapper { Language = lang, StringValue = lang.GetTranslatedString(EnumsLocalizer) });

        authUser = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User;
        userEmail = authUser?.FindFirst("preferred_username")?.Value!;

        await GetSupervisorsAndTopicsData();
        SortTopics();
        SortSupervisors();
    }

    private async Task GetSupervisorsAndTopicsData()
    {
        topics = (await httpClient.Client.GetFromJsonAsync<IEnumerable<Topic>>(ApiEndpoints.Topics))!;
        if (topics == null)
            throw new Exception("Could not load topics");

        topicsInDropdownList = topics;

        coSupervisors = (await httpClient.Client.GetFromJsonAsync<IEnumerable<Supervisor>>(ApiEndpoints.Supervisors))!.Where(supervisor => supervisor.Email != userEmail);
        if (coSupervisors == null)
            throw new Exception("Could not load supervisors");
    }

    private void OnTopicSelectedInList(object value)
    {
        if (value is not null)
        {
            string val = (string)value;
            projectTopics.Add(topicsInDropdownList.Single(t => t.Name == val));
            topicsInDropdownList = topicsInDropdownList.Where(t => t.Name != val);
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
        topicsInDropdownList = topicsInDropdownList.Append(topic);
        SortTopics();
    }

    private void OnSelectedCoSupervisorClicked(Supervisor coSupervisor)
    {
        projectCoSupervisor = null;
        coSupervisors = coSupervisors.Append(coSupervisor);
        SortSupervisors();
    }

    private void SortTopics() => topicsInDropdownList = topicsInDropdownList.OrderBy(t => t.Category.ToString()).ThenBy(t => t.Name);

    private void SortSupervisors() => coSupervisors = coSupervisors.OrderBy(s => s.FullName);

    private void OnAddNewTopicFromSearchClicked() 
    {
        if (!string.IsNullOrWhiteSpace(topicName)) {
            if (topicsInDropdownList.Select(topic => topic.Name).Contains(topicName, StringComparer.OrdinalIgnoreCase))
            {
                var newTopic = topicsInDropdownList.Single(topic => topic.Name.Equals(topicName, StringComparison.OrdinalIgnoreCase));
                projectTopics.Add(newTopic);
                topicsInDropdownList = topicsInDropdownList.Where(topic => topic.Name != newTopic.Name);
                topicSelector?.Reset();
            }
            if (topicName.Length > 25)
            {
                JSRuntime.InvokeAsync<string>("alert", "Topic should not be more than 25 characters");
                topicName = string.Empty;
            }
            else
                projectTopics.Add(new Topic { Name = topicName });
        }
        topicName = string.Empty;
    }

    private void AssignCoSupervisorIfNotNull()
    {
        project.CoSupervisor = projectCoSupervisor is not null 
            ? new Supervisor 
            { 
                Id = projectCoSupervisor.Id, 
                FirstName = projectCoSupervisor.FirstName, 
                LastName = projectCoSupervisor.LastName 
            } 
            : null;
    }

    private async Task SubmitProjectAsync()
    {
        if (projectEcts is null || projectProgrammes is null || projectLanguages is null || projectTopics.Count == 0 || projectCoSupervisor is null || project.Title is null || project.Semester is null)
        {
            await JSRuntime.InvokeAsync<string>("alert", "Something went wrong! Please make sure to fill out all required fields and try again.");
            return;
        }

        var newProject = new ProjectCreateDto()
        {
            Title = project.Title,
            DescriptionHtml = descriptionHtml!,
            Topics = projectTopics.Select(t => new Topic { Name = t.Name, Category = t.Category }),
            Languages = projectLanguages!,
            Programmes = projectProgrammes!,
            Ects = (Ects)projectEcts!,
            Semester = project.Semester,
            SupervisorEmail = userEmail!,
            CoSupervisorEmail = projectCoSupervisor?.Email
        };

        IEnumerable<Topic> newTopics = projectTopics.Where(topic => topic.Category is null);

        if (newTopics.Any())
        {
            // A new topic was added, open dialog to confirm and to add category.
                var result = await SelectTopicCategoryDialog(newTopics);

            // Check if the dialog was confirmed (Save button clicked)
            // and update the project's topics with the modified newProject topics.
            if (result == true)
            {
                project.Topics = newProject.Topics.ToList();
            }
            if (result == null)
            {
                return;
            }

            else
            {
                // If the dialog was canceled (Cancel button clicked), do not post the project.
                return;
            }
        }

        if (project.Title.Length > 50)
        {
            await JSRuntime.InvokeAsync<string>("alert", "Project title should not be more than 50 characters.");
            project.Title = string.Empty;
        }
        if (newProject.DescriptionHtml.Length > 4800)
        {
            await JSRuntime.InvokeAsync<string>("alert", "Project description should not be more than 2400 characters.");
        }
        else
        {
            try
            {
                var response = await httpClient.Client.PostAsJsonAsync(ApiEndpoints.Projects, newProject);

                if (response.IsSuccessStatusCode)
                {
                    await JSRuntime.InvokeAsync<string>("alert", "Project created successfully!");
                    navManager.NavigateTo(PageUrls.MyProjects);
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
    }

    private void CancelProjectAsync() => navManager.NavigateTo(PageUrls.MyProjects);

    private async Task GenerateTopicsFromDiscription() 
    {
        try
        {
            if (!string.IsNullOrEmpty(descriptionHtml) && descriptionHtml.Length> 1500)
            {
                var query = "create one worded relevant topics from above description and put it in a json list using follwoing json structure: " +
                "[{\"Name\": \"Risk Assessment\"}]. " +
                "Each topic should not have more than 25 characters."+
                "dont create more than 7 topic";

                var strippedString = Regex.Replace(descriptionHtml, "<[^>]*>", " ");
                foreach (var (key, val) in _htmlEntitiesTable)
                {
                    strippedString = strippedString.Replace(key, val);
                }

                var response = await httpClient.Client.PostAsJsonAsync(ApiEndpoints.Gpt, strippedString + " " + query);
                var filterout = response.Content.ReadAsStringAsync();
                var resutl = filterout.Result;

                var aiTopics = JsonConvert.DeserializeObject<List<Topic>>(resutl);

                if (aiTopics != null && aiTopics.Count > 0)
                {
                    
                    projectTopics.AddRange(aiTopics);
                }

                if (response.IsSuccessStatusCode)
                {
                    await JSRuntime.InvokeAsync<string>("alert", "Topics created");
                    //navManager.NavigateTo(PageUrls.MyRequests);
                }
                else
                {
                    await JSRuntime.InvokeAsync<string>("alert", "Something went wrong!");
                }

            }
            else
            {
                await JSRuntime.InvokeAsync<string>("alert", "Something went wrong! Please make sure the description is longer than 1500 characters.");
            }

        }
        catch
        {
            await JSRuntime.InvokeAsync<string>("alert", "Something went wrong! Please make sure to fill out all required fields and try again.");
        }

    }

    private async Task GenerateTitleFromDescription() 
    {
        try
        {
            if (!string.IsNullOrEmpty(descriptionHtml) && descriptionHtml.Length > 1500)
            {
                var query = "create project title from above description and return it as string. Project title should not be more than 50 characters";

                var strippedString = Regex.Replace(descriptionHtml, "<[^>]*>", " ");
                foreach (var (key, val) in _htmlEntitiesTable)
                {
                    strippedString = strippedString.Replace(key, val);
                }

                var response = await httpClient.Client.PostAsJsonAsync(ApiEndpoints.Gpt, strippedString + " " + query);
                var filterout = response.Content.ReadAsStringAsync();
                var resutl = filterout.Result;

                project.Title = resutl;

                if (response.IsSuccessStatusCode)
                {
                    await JSRuntime.InvokeAsync<string>("alert", "title created");
                    //navManager.NavigateTo(PageUrls.MyRequests);
                }
                else
                {
                    await JSRuntime.InvokeAsync<string>("alert", "Something went wrong!");
                }

            }
            else
            {
                await JSRuntime.InvokeAsync<string>("alert", "Something went wrong! Please make sure the description is longer than 1500 characters.");
            }

        }
        catch
        {
            await JSRuntime.InvokeAsync<string>("alert", "Something went wrong! Please make sure to fill out all required fields and try again.");
        }
    }

    
}