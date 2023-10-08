using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using ProjectIT.Client.Constants;
using ProjectIT.Client.Shared.Helpers;
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

    // All topics in database.
    private IEnumerable<Topic> topics = null!;

    // All topics available in the dropdown.
    private IEnumerable<Topic> topicsInDropdownList = null!;

    private IEnumerable<Supervisor> coSupervisors = null!;
    private IEnumerable<EctsWrapper>? ectsWrappers;
    private IEnumerable<ProgrammeWrapper>? programmeWrappers;
    private IEnumerable<LanguageWrapper>? languageWrappers;

    private ProjectUpdateDto? projectToBeUpdated = new();
    private List<Topic>? projectTopics = new();
    private Supervisor? projectCoSupervisor = null!;

    private RadzenDropDown<Topic>? topicSelector;
    private RadzenDropDown<Supervisor>? coSupervisorSelector;

    private string? topicName;

    private ClaimsPrincipal? authUser;
    private string? userEmail;
    private bool isLoading = false;

    protected override async Task OnInitializedAsync()
    {
        ectsWrappers = Enum.GetValues<Ects>().Select(ects => new EctsWrapper { Ects = ects, StringValue = ects.GetTranslatedString(EnumsLocalizer) });
        programmeWrappers = Enum.GetValues<Programme>().Select(prog => new ProgrammeWrapper { Programme = prog, StringValue = prog.GetTranslatedString(EnumsLocalizer) });
        languageWrappers = Enum.GetValues<Language>().Select(lang => new LanguageWrapper { Language = lang, StringValue = lang.GetTranslatedString(EnumsLocalizer) });

        authUser = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User;
        userEmail = authUser?.FindFirst("preferred_username")?.Value!;

        projectToBeUpdated = await httpClient.Client.GetFromJsonAsync<ProjectUpdateDto>($"{ApiEndpoints.Projects}/{Id}");

        if (projectToBeUpdated?.Topics is not null && projectToBeUpdated?.Topics.Count() > 0)
            projectTopics = projectToBeUpdated?.Topics?.ToList();
        
        projectCoSupervisor = projectToBeUpdated?.CoSupervisor;
        
        await GetSupervisorsAndTopicsData();
        SortTopics();
        SortSupervisors();
    }

    private async Task GetSupervisorsAndTopicsData()
    {
        topics = (await httpClient.Client.GetFromJsonAsync<IEnumerable<Topic>>(ApiEndpoints.Topics))!;
        if (topics == null)
            throw new Exception("Could not load topics");

        if (projectToBeUpdated?.Topics is not null)
            topicsInDropdownList = topics.Except(projectToBeUpdated!.Topics!);
        else
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
            projectTopics?.Add(topicsInDropdownList.Single(t => t.Name == val));
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
        projectTopics?.Remove(topic);
        if (topic.Category is not null)
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

    private async Task OnAddNewTopicFromSearchClicked()
    {
        if (!string.IsNullOrWhiteSpace(topicName))
        {
            if (topicName.Length > 25)
            {
                await JSRuntime.InvokeAsync<string>("alert", "Topic name cannot be more than 25 characters");
                topicName = string.Empty;
            }
            if (topicsInDropdownList.Select(topic => topic.Name).Contains(topicName, StringComparer.OrdinalIgnoreCase))
            {
                var newTopic = topicsInDropdownList.Single(topic => topic.Name.Equals(topicName, StringComparison.OrdinalIgnoreCase));
                projectTopics?.Add(newTopic);
                topicsInDropdownList = topicsInDropdownList.Where(topic => topic.Name != newTopic.Name);
                topicSelector?.Reset();
            }
            else
            {
                projectTopics?.Add(new Topic { Name = topicName });
            }
        }
        topicName = string.Empty;
    }

    private async Task SubmitProjectAsync()
    {
        if (projectToBeUpdated!.Programmes.IsNullOrEmpty() || projectToBeUpdated.Languages.IsNullOrEmpty() || projectToBeUpdated.Title is null || projectToBeUpdated.Semester is null)
        {
            await JSRuntime.InvokeAsync<string>("alert", "Something went wrong! Please make sure to fill out all required fields and try again.");
            return;
        }

        var updatedProject = new ProjectUpdateDto()
        {
            Id = projectToBeUpdated!.Id,
            Title = projectToBeUpdated!.Title,
            DescriptionHtml = projectToBeUpdated.DescriptionHtml,
            Topics = projectTopics?.Select(t => new Topic { Name = t.Name, Category = t.Category }),
            Languages = projectToBeUpdated.Languages,
            Programmes = projectToBeUpdated.Programmes,
            Ects = projectToBeUpdated.Ects,
            Semester = projectToBeUpdated.Semester,
            CoSupervisor = projectCoSupervisor
        };

        IEnumerable<Topic>? newTopics = projectTopics?.Where(topic => topic.Category is null);

        if (newTopics is not null && newTopics.Any())
        {
            // A new topic was added, open dialog to confirm and to add category.
            var result = await SelectTopicCategoryDialog(newTopics);

            // Check if the dialog was confirmed (Save button clicked)
            // and update the project's topics with the modified newProject topics.
            if (result == true)
            {
                projectToBeUpdated.Topics = updatedProject.Topics?.ToList();
            }
            else
            {
                // If the dialog was canceled (Cancel button clicked), do not post the project.
                return;
            }
        }

        if (updatedProject.Title.Length > EntityPropertyRestrictions.ProjectTitleCap)
        {
            await JSRuntime.InvokeAsync<string>("alert", $"Project title should not be more than {EntityPropertyRestrictions.ProjectTitleCap} characters.");
            return;
        }

        var strippedString = HTMLHelper.RemoveTagsFromString(updatedProject!.DescriptionHtml);
        if (strippedString.Length > EntityPropertyRestrictions.ProjectDescriptionCap)
        {
            await JSRuntime.InvokeAsync<string>("alert", $"Project description should not be more than {EntityPropertyRestrictions.ProjectDescriptionCap} characters.");
            return;
        }

        try
        {
            var response = await httpClient.Client.PutAsJsonAsync($"{ApiEndpoints.Projects}/{Id}", updatedProject);

            if (response.IsSuccessStatusCode)
            {
                await JSRuntime.InvokeAsync<string>("alert", "Project updated successfully!");
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

    private void CancelProjectAsync() => navManager.NavigateTo(PageUrls.MyProjects);

    //this methods is using chat gpt to generate topics for the project description
    private async Task GenerateTopicsFromDescription() 
    {
        try
        {
            if (!string.IsNullOrEmpty(projectToBeUpdated!.DescriptionHtml) && projectToBeUpdated.DescriptionHtml.Length> 1500)
            {
                //creating the query 
                var query = "create one worded relevant topics from above description and put it in a json list using follwoing json structure: " +
                "[{\"Name\": \"Risk Assessment\"}]. " +
                "Each topic should not have more than 25 characters."+
                "dont create more than 7 topic";

                var strippedString = HTMLHelper.RemoveTagsFromString(projectToBeUpdated.DescriptionHtml);

                isLoading = true;
                // calling the chat gbt api using the description and the query
                var response = await httpClient.Client.PostAsJsonAsync(ApiEndpoints.Gpt, strippedString + " " + query);
                var filterout = response.Content.ReadAsStringAsync();
                var resutl = filterout.Result;

                var aiTopics = JsonConvert.DeserializeObject<List<Topic>>(resutl);
                isLoading = false;

                if (aiTopics != null && aiTopics.Count > 0)
                {

                    projectTopics?.AddRange(aiTopics);
                }

                if (response.IsSuccessStatusCode)
                {
                    await JSRuntime.InvokeAsync<string>("alert", "Topics created");
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

    //this method is creating chat gpt to create title for the project description.
    private async Task GenerateTitleFromDescription() 
    {
        try
        {
            if (!string.IsNullOrEmpty(projectToBeUpdated!.DescriptionHtml) && projectToBeUpdated.DescriptionHtml.Length > 1500)
            {
                var query = "create project title from above description and return it as string. Project title should not be more than 50 characters";
                
                var strippedString = HTMLHelper.RemoveTagsFromString(projectToBeUpdated.DescriptionHtml);
                isLoading = true;
                var response = await httpClient.Client.PostAsJsonAsync(ApiEndpoints.Gpt, strippedString + " " + query);
                var filterout = response.Content.ReadAsStringAsync();
                var resutl = filterout.Result;

                projectToBeUpdated!.Title = resutl;
                isLoading = false;

                if (response.IsSuccessStatusCode)
                {
                    await JSRuntime.InvokeAsync<string>("alert", "Title created");
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